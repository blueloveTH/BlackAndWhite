using UnityEngine;

namespace FogOfWar
{
    public sealed class FOWSystem : FOWSystemBase
    {
        private Vector3 localBasePoint;
        protected override Vector2 worldSize { get { return ToPlane2D(transform.localScale); } }

        protected override void Awake()
        {
            base.Awake();
            localBasePoint = transform.position;
            localBasePoint.x -= worldSize.x * 0.5f;
            localBasePoint.z -= worldSize.y * 0.5f;
        }


        protected override Vector2 WorldPos2Tex(Vector3 wPos)
        {
            return ToPlane2D(wPos - localBasePoint) * worldToTex;
        }

        protected override void OnDrawGizmosSelected()
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(Vector2.zero, Vector2.one);
        }

    }
}