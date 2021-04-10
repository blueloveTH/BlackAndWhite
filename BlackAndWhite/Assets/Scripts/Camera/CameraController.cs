using GameFlow;
using UnityEngine;

[RequireComponent(typeof(Camera))]
[DisallowMultipleComponent()]
public class CameraController : MonoBehaviour
{
    protected Camera cameraCpnt { get; private set; }

    protected virtual void Awake()
    {
        cameraCpnt = GetComponent<Camera>();
    }
    /*
    public static Bounds GetOrthoCamBounds2D(CubeRange range)
    {
        return GetOrthoCamBounds2D(range, Camera.main.orthographicSize, Camera.main.aspect);
    }

    public static Bounds GetOrthoCamBounds2D(CubeRange range, float orthoSize, float aspect)
    {
        Vector2 v = new Vector2(orthoSize * aspect, orthoSize);
        Vector2 min = (Vector2)range.min + v;
        Vector2 max = (Vector2)range.max - v;
        return new Bounds(range.center.OverrideZ(0), new Vector3(max.x - min.x, max.y - min.y));
    }*/
}
