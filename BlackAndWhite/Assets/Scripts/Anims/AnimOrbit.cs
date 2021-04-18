using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimOrbit : MonoBehaviour
{
    public bool use2D = false;
    public Transform center;
    public float speed = 30;

    private void Reset()
    {
        center = transform.parent;
    }


    private void LateUpdate()
    {
        Vector3 axis = use2D ? -center.forward : center.up;
        transform.RotateAround(center.position, axis, speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        if (center == null) return;
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(center.position, Vector2.Distance(center.position, transform.position));
    }

    private void OnDrawGizmosSelected()
    {
        if (center == null) return;
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(center.position, Vector2.Distance(center.position, transform.position));
    }
}
