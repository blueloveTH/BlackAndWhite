/*
using GameFlow;
using UnityEngine;

public class CameraTracking2D : CameraController
{
    public Transform target;
    [Range(0.1f, 1f)] public float property = 0.2f;

    [SerializeField] private CubeRange rectRange;

    public Vector2 cameraOffset;
    private Vector2 _smoothDampVelocity;

    void LateUpdate()
    {
        if (target == null) return;
        Vector2 targetPos = (Vector2)target.position + cameraOffset;
        Vector3 nextPos = Vector2.SmoothDamp(transform.position, targetPos, ref _smoothDampVelocity, property);

        if (rectRange != null)
        {
            var bounds = GetOrthoCamBounds2D(rectRange, cameraCpnt.orthographicSize, cameraCpnt.aspect);
            nextPos.x = Mathf.Clamp(nextPos.x, bounds.min.x, bounds.max.x);
            nextPos.y = Mathf.Clamp(nextPos.y, bounds.min.y, bounds.max.y);
        }
        transform.position = nextPos.OverrideZ(transform.position.z);
    }

    public void ResetPos()
    {
        if (target != null)
        {
            Vector3 targetPos = target.position + (Vector3)cameraOffset;
            transform.position = targetPos.OverrideZ(transform.position.z);
        }
    }

}
*/
