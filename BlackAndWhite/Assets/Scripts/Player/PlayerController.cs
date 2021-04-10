using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerActor actor;

    private void Awake()
    {
        actor = transform.parent.GetComponent<PlayerActor>();
    }

    private void Start()
    {
        actor.OnUpdate += Actor_OnUpdate;
    }

    private void Actor_OnUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        actor.velocity = new Vector2(h, v) * actor.speed;
    }
}
