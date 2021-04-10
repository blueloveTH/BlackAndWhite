using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace FogOfWar
{
    public enum RenderMode
    {
        PlaneMask,
        //Projector
    }

    [DefaultExecutionOrder(-100)]
    public abstract class FOWSystemBase : MonoBehaviour
    {
        public enum State
        {
            Blending,
            NeedUpdate,
            UpdateTexture,
        }

        public static Vector2 ToPlane2D(Vector3 v3d)
        {
            return new Vector2(v3d.x, v3d.z);
        }

        public static Vector2Int RoundToInt(Vector2 v)
        {
            return new Vector2Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y));
        }

        private FOWRender _render;
        private Vector2Int _textureSize = Vector2Int.zero;
        private float mNextUpdate = 0f;
        private State mState = State.Blending;
        private Thread mThread;
        private volatile bool mThreadWork;
        private float mElapsed = 0f;

        // Color buffers -- prepared on the worker thread.
        private Color32[] mBuffer0;
        private Color32[] mBuffer1;
        private Color32[] mBuffer2;

        List<FOWAbstractRevealer> mRevealers = new List<FOWAbstractRevealer>();
        HashSet<FOWAbstractRevealer> mAdded = new HashSet<FOWAbstractRevealer>();
        HashSet<FOWAbstractRevealer> mRemoved = new HashSet<FOWAbstractRevealer>();

        protected FOWRender render {
            get {
                if (_render == null)
                {
                    switch (renderMode)
                    {
                        //case RenderMode.Projector:
                        //    _render = GetComponentInChildren<Projector>(true).GetComponent<FOWRender>();
                        //    break;
                        case RenderMode.PlaneMask:
                            _render = GetComponentInChildren<MeshRenderer>(true).GetComponent<FOWRender>();
                            break;
                    }
                }
                return _render;
            }
        }
        protected abstract Vector2 worldSize { get; }

        [SerializeField] RenderMode _renderMode = RenderMode.PlaneMask;
        [SerializeField] bool useMultiThread = true;
        [SerializeField] [Range(0.1f, 4f)] float _textureQuality = 2;
        [Range(0.02f, 0.64f)] [SerializeField] float updateFrequency = 0.2f;
        [Range(0, 1)] [SerializeField] float textureBlendTime = 0.5f;
        [Range(0, 4)] [SerializeField] int blurIterations = 2;

        public static FOWSystemBase Main { get; private set; }
        public float worldToTex { get { return _textureQuality; } }
        public Vector2Int textureSize {
            get {
                if (_textureSize == Vector2Int.zero)
                    _textureSize = RoundToInt(worldToTex * worldSize);
                return _textureSize;
            }
        }
        public RenderMode renderMode { get { return _renderMode; } }
        public float blendFactor { get; private set; }
        internal Texture2D fogTexture { get; private set; }

        public void AddRevealer(FOWAbstractRevealer rev) { if (rev != null) lock (mAdded) mAdded.Add(rev); }
        public void DelRevealer(FOWAbstractRevealer rev) { if (rev != null) lock (mRemoved) mRemoved.Add(rev); }

        protected virtual void Awake()
        {
            Main = this;
            mBuffer0 = new Color32[textureSize.x * textureSize.y];
            mBuffer1 = new Color32[mBuffer0.Length];
            mBuffer2 = new Color32[mBuffer0.Length];

#if UNITY_WEBGL
            useMultiThread = false;
#endif
            if (useMultiThread) CreateThread();

            UpdateTexture();
            render.gameObject.SetActive(true);
        }

        private void CreateThread()
        {
            // Add a thread update function -- all visibility checks will be done on a separate thread
            mThread = new Thread(ThreadUpdate);
            mThreadWork = true;
            mThread.Start();
        }

        private void Update()
        {
            if (textureBlendTime > 0f) blendFactor = Mathf.Clamp01(blendFactor + Time.deltaTime / textureBlendTime);
            else blendFactor = 1f;

            if (!useMultiThread)
            {
                if (mState == State.NeedUpdate)
                {
                    UpdateBuffer();
                    mState = State.UpdateTexture;
                }
            }

            if (mState == State.Blending)
            {
                float time = Time.time;

                if (mNextUpdate < time)
                {
                    mNextUpdate = time + updateFrequency;
                    mState = State.NeedUpdate;
                }
            }
            else if (mState != State.NeedUpdate) UpdateTexture();
        }


        private void OnDestroy()
        {
            if (mThread != null)
            {
                mThreadWork = false;
                mThread.Join();
                mThread = null;
            }
            mBuffer0 = null;
            mBuffer1 = null;
            mBuffer2 = null;
            if (fogTexture != null)
            {
                Destroy(fogTexture);
                fogTexture = null;
            }
        }

        private void ThreadUpdate()
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

            while (mThreadWork)
            {
                if (mState == State.NeedUpdate)
                {
                    sw.Reset();
                    sw.Start();
                    UpdateBuffer();
                    sw.Stop();
                    mElapsed = 0.001f * sw.ElapsedMilliseconds;
                    mState = State.UpdateTexture;
                }
                Thread.Sleep(1);
            }
        }

        private void UpdateBuffer()
        {
            // Add all items scheduled to be added
            if (mAdded.Count > 0)
            {
                lock (mAdded)
                {
                    mRevealers.AddRange(mAdded);
                    mAdded.Clear();
                }
            }

            // Remove all items scheduled for removal
            if (mRemoved.Count > 0)
            {
                lock (mRemoved)
                {
                    mRevealers.RemoveAll((x) => mRemoved.Contains(x));
                    mRemoved.Clear();
                }
            }

            // Use the texture blend time, thus estimating the time this update will finish
            // Doing so helps avoid visible changes in blending caused by the blended result being X milliseconds behind.
            float factor = (textureBlendTime > 0f) ? Mathf.Clamp01(blendFactor + mElapsed / textureBlendTime) : 1f;

            // Clear the buffer's red channel (channel used for current visibility -- it's updated right after)
            for (int i = 0, imax = mBuffer0.Length; i < imax; i++)
            {
                mBuffer0[i] = Color32.Lerp(mBuffer0[i], mBuffer1[i], factor);
                mBuffer1[i].r = 0;
            }

            // Update the visibility buffer, one revealer at a time
            for (int i = 0; i < mRevealers.Count; i++) if (mRevealers[i].isEnabled) PaintCircle(mRevealers[i]);

            // Blur the final visibility data
            for (int i = 0; i < blurIterations; i++) Blur();

            // Reveal the map based on what's currently visible
            for (int i = 0; i < mBuffer0.Length; i++)
            {
                if (mBuffer1[i].g < mBuffer1[i].r) mBuffer1[i].g = mBuffer1[i].r;
                mBuffer0[i].b = mBuffer1[i].r;
                mBuffer0[i].a = mBuffer1[i].g;
            }
        }


        private void PaintCircle(FOWAbstractRevealer r)
        {
            PaintCircle(r.worldPos, r.radius);
        }

        private void PaintCircle(Vector3 worldPos, float radius)
        {
            // Position relative to the fog of war
            Vector2 pos = WorldPos2Tex(worldPos);

            float r = radius * worldToTex;

            // Coordinates we'll be dealing with
            int xmin = Mathf.RoundToInt(pos.x - r);
            int ymin = Mathf.RoundToInt(pos.y - r);
            int xmax = Mathf.RoundToInt(pos.x + r);
            int ymax = Mathf.RoundToInt(pos.y + r);

            int cx = Mathf.RoundToInt(pos.x);
            int cy = Mathf.RoundToInt(pos.y);

            cx = Mathf.Clamp(cx, 0, textureSize.x - 1);
            cy = Mathf.Clamp(cy, 0, textureSize.y - 1);

            int radiusSqr = Mathf.RoundToInt(r * r);

            if (ymin < 0) ymin = 0;
            if (xmin < 0) xmin = 0;
            if (xmax > textureSize.x - 1) xmax = textureSize.x - 1;
            if (ymax > textureSize.y - 1) ymax = textureSize.y - 1;

            for (int y = ymin; y <= ymax; y++)
            {
                int yw = y * textureSize.x;
                for (int x = xmin; x <= xmax; ++x)
                {
                    int xd = x - cx;
                    int yd = y - cy;
                    int dist = xd * xd + yd * yd;

                    // Reveal this pixel
                    if (dist <= radiusSqr) mBuffer1[x + yw].r = 255;
                }
            }
        }

        private void Blur()
        {
            Color32 c;
            for (int y = 1; y < textureSize.y - 1; y++)
            {
                int yw = y * textureSize.x;
                int yw0 = (y - 1) * textureSize.x;
                int yw1 = (y + 1) * textureSize.x;

                for (int x = 1; x < textureSize.x - 1; ++x)
                {
                    int x0 = x - 1;
                    int x1 = x + 1;

                    int val = mBuffer1[x + yw].r;

                    val += mBuffer1[x0 + yw].r;
                    val += mBuffer1[x1 + yw].r;
                    val += mBuffer1[x + yw0].r;
                    val += mBuffer1[x + yw1].r;

                    val += mBuffer1[x0 + yw0].r;
                    val += mBuffer1[x1 + yw0].r;
                    val += mBuffer1[x0 + yw1].r;
                    val += mBuffer1[x1 + yw1].r;

                    c = mBuffer2[x + yw];
                    c.r = (byte)(val / 9);
                    mBuffer2[x + yw] = c;
                }
            }

            // Swap the buffer so that the blurred one is used
            Color32[] temp = mBuffer1;
            mBuffer1 = mBuffer2;
            mBuffer2 = temp;
        }

        private void UpdateTexture()
        {
            if (fogTexture == null)
            {
                // Native ARGB format is the fastest as it involves no data conversion
                fogTexture = new Texture2D(textureSize.x, textureSize.y, TextureFormat.ARGB32, false);

                fogTexture.wrapMode = TextureWrapMode.Clamp;
                fogTexture.SetPixels32(mBuffer0);
                fogTexture.Apply();
                mState = State.Blending;
            }
            else if (mState == State.UpdateTexture)
            {
                fogTexture.SetPixels32(mBuffer0);
                fogTexture.Apply();
                blendFactor = 0f;
                mState = State.Blending;
            }
        }

        protected void RenderImmediately()
        {
            UpdateBuffer();
            mState = State.UpdateTexture;
            UpdateTexture();
            blendFactor = 1.0f;
        }
        protected abstract Vector2 WorldPos2Tex(Vector3 wPos);
        protected abstract void OnDrawGizmosSelected();

        public bool IsVisible(Vector3 wPos)
        {
            Vector2 tPos = WorldPos2Tex(wPos);
            int cx = Mathf.RoundToInt(tPos.x);
            int cy = Mathf.RoundToInt(tPos.y);

            cx = Mathf.Clamp(cx, 0, textureSize.x - 1);
            cy = Mathf.Clamp(cy, 0, textureSize.y - 1);
            int index = cx + cy * textureSize.x;
            return mBuffer0[index].r > 64 || mBuffer1[index].r > 0;
        }
        public bool IsExplored(Vector3 wPos)
        {
            Vector2 tPos = WorldPos2Tex(wPos);

            int cx = Mathf.RoundToInt(tPos.x);
            int cy = Mathf.RoundToInt(tPos.y);

            cx = Mathf.Clamp(cx, 0, textureSize.x - 1);
            cy = Mathf.Clamp(cy, 0, textureSize.y - 1);
            return mBuffer0[cx + cy * textureSize.x].g > 0;
        }
    }
}