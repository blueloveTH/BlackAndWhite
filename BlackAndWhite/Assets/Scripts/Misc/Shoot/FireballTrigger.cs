using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFlow;

public class FireballTrigger : InteractiveBehaviour
{
    [SerializeField] GameObject vfx;

    private void Awake()
    {
        Destroy(gameObject, 4f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.Find("Vision")?.GetComponent<FadeVision>().DetachAndFade();

        gameObject.SetActive(false);
        Destroy(gameObject, 0.16f);

        Instantiate(vfx, transform.position, transform.rotation); 
    }
}
