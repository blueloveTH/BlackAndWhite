using UnityEngine;


public class AnimSwing : MonoBehaviour
{
    public float A = 2.7f;                //acceleration of gravity divide the length

    private float currSpeed = 0f;
    private float currAcceleration;

    // Update is called once per frame
    void FixedUpdate()
    {
        currAcceleration = A * Mathf.Sin(transform.eulerAngles.z / 180f * Mathf.PI);
        currSpeed += currAcceleration * Time.fixedDeltaTime;
        transform.Rotate(new Vector3(0, 0, -currSpeed));
    }
}

