using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : RigidbodyActor2D
{
    public float speed = 5;
    [SerializeField] SpriteRenderer spRenderer;
    [SerializeField] WeaponSlot weaponSlot;

    protected override void InternalUpdate()
    {
        if (Mathf.Abs(velocity.x) > 0.01f)
        {
            spRenderer.flipX = Mathf.Sign(velocity.x) < 0;
            weaponSlot.transform.localScale = new Vector3(Mathf.Sign(velocity.x), 1, 1);
        }
    }
}
