using UnityEngine;


public class AnimRotate : MonoBehaviour
{
    public Vector3 velocity = new Vector3(0, 0, 30);

    void LateUpdate()
    {
        transform.Rotate(velocity * Time.deltaTime);
    }
}
