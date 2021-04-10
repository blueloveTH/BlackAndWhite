using UnityEngine;

public abstract class BaseActor2D : MonoBehaviour
{

    public abstract Vector2 position { get; set; }
    public abstract Vector2 velocity { get; set; }

    public event System.Action OnUpdate;

    private void Update()
    {
        InternalUpdate();
        OnUpdate?.Invoke();
    }

    protected abstract void InternalUpdate();

    protected abstract void OnEnable();
    protected abstract void OnDisable();
}
