using UnityEngine;


public class CameraFollow : CameraController
{
    public Transform target;
    const float smoothDampTime = 0.32f;
    private Vector3 currVelocity;
    [SerializeField] private Vector3 offset;
    [SerializeField] bool noSmooth;

    public void Reset()
    {
        if (target != null)
            offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        if (target == null) return;
        if (noSmooth)
        {
            transform.position = target.position + offset;
            return;
        }
        transform.position = Vector3.SmoothDamp(transform.position, target.position + offset, ref currVelocity, smoothDampTime);
    }

    public void Align()
    {
        if (target)
            transform.position = target.position + offset;
    }
}
