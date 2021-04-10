using UnityEngine;

namespace FogOfWar
{
    public sealed class FOWSystem2D : FOWSystemBase
    {
        private Vector2 localBasePoint;
        protected override Vector2 worldSize { get { return transform.localScale; } }

        protected override void Awake()
        {
            base.Awake();
            localBasePoint = ToPlane2D(transform.position) - worldSize * 0.5f;
        }

        protected override Vector2 WorldPos2Tex(Vector3 wPos)
        {
            return ((Vector2)wPos - localBasePoint) * worldToTex;
        }

        protected override void OnDrawGizmosSelected()
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(Vector2.zero, Vector2.one);
        }
    }
}