using UnityEngine;

public abstract class AnimMotion : MonoBehaviour
{
    [SerializeField] protected float startTime = 2f;
    [SerializeField] protected float interval = 2f;
    [SerializeField] protected float duration = 4f;
    [SerializeField] protected Vector2 targetVector = Vector2.zero;
    [SerializeField] protected Vector2 flipScale = Vector2.one;

    protected float direction = 1f;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)targetVector);
    }
}
