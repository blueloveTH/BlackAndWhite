using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerActor actor;
    [SerializeField] WeaponSlot weaponSlot;

    private void Start()
    {
        actor.OnUpdate += Actor_OnUpdate;
    }

    private void Actor_OnUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        actor.velocity = new Vector2(h, v) * actor.speed;

        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.K))
        {
            weaponSlot.Attack();
        }
    }
}
