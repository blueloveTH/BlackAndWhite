using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController main { get; private set; }

    [SerializeField] PlayerActor actor;
    [SerializeField] WeaponSlot weaponSlot;

    private void Start()
    {
        main = this;
    }

    private void OnEnable()
    {
        actor.OnUpdate += Actor_OnUpdate;
    }

    private void OnDisable()
    {
        actor.OnUpdate -= Actor_OnUpdate;
        actor.Move(0, 0);
    }

    private void Actor_OnUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        actor.Move(h, v);

        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.K))
        {
            weaponSlot.Attack();
        }
    }
}
