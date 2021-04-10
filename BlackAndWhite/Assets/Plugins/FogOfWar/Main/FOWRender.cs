using UnityEngine;

namespace FogOfWar
{
    public class FOWRender : MonoBehaviour
    {
        private Material mat;
        private FOWSystemBase _system;

        protected FOWSystemBase system {
            get {
                if (_system == null) _system = transform.GetCpntInDirectParent<FOWSystemBase>();
                return _system;
            }
        }

        public Color unexploredColor = new Color(0f, 0f, 0f, 250f / 255f);
        public Color exploredColor = new Color(0f, 0f, 0f, 200f / 255f);

        void Start()
        {
            switch (system.renderMode)
            {
                //case RenderMode.Projector: mat = GetComponent<Projector>().material; break;
                case RenderMode.PlaneMask: mat = GetComponent<MeshRenderer>().material; break;
            }
            if (mat == null) enabled = false;
            LateUpdate();
        }

        void LateUpdate()
        {
            mat.SetTexture("_MainTex", system.fogTexture);
            mat.SetFloat("_BlendFactor", system.blendFactor);
            mat.SetColor("_Unexplored", unexploredColor);
            mat.SetColor("_Explored", exploredColor);
        }
    }
}