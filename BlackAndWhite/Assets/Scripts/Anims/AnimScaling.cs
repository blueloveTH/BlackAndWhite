using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimScaling : MonoBehaviour
{
    Vector3 initialScale;
    [SerializeField] Vector2 range = new Vector2(0.75f, 1.1f);
    [SerializeField] float speed = 1f;

    private void Start()
    {
        initialScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        float s = Mathf.PingPong(Time.time * speed, range.y - range.x) + range.x;
        transform.localScale = initialScale * s;
    }
}
