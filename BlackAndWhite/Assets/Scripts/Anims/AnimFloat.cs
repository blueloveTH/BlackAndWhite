using UnityEngine;


public class AnimFloat : MonoBehaviour
{
    public Axis axis = Axis.Y;
    public Space space = Space.World;
    public float amplitude = 1f;
    public float speed = 1;

    public enum Axis
    {
        X, Y
    }

    private float time = 0f;
    private void FixedUpdate()
    {
        float previous = time;
        time += Time.fixedDeltaTime * speed;
        float deltaHeight = Mathf.Sin(time) - Mathf.Sin(previous);

        Vector3 delta = (axis == Axis.Y ? Vector3.up : Vector3.right) * deltaHeight * amplitude;

        if (space == Space.Self)
            transform.localPosition += delta;
        else
            transform.position += delta;
    }
}

