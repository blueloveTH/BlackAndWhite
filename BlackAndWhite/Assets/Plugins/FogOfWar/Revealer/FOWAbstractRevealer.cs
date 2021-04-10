using UnityEngine;

namespace FogOfWar
{
    public abstract class FOWAbstractRevealer : MonoBehaviour
    {
        public bool isEnabled = true;
        public float radius = 2f;
        public Vector3 worldPos;

        private void Awake()
        {
            if (FOWSystemBase.Main == null || FOWSystemBase.Main.enabled == false)
            {
                enabled = false;
                return;
            }
            OnAwake();
        }

        protected abstract void OnAwake();

        private void OnEnable()
        {
            if (FOWSystemBase.Main != null)
                FOWSystemBase.Main.AddRevealer(this);
        }
        private void OnDisable()
        {
            if (FOWSystemBase.Main != null)
                FOWSystemBase.Main.DelRevealer(this);
        }
        void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(0, 0.6f, 1, 0.7f);
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
