using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : RigidbodyActor2D
{
    [SerializeField] protected float speed = 9;

    protected override void OnEnable()
    {
        base.OnEnable();
        velocity = transform.right * speed;
    }

    protected override void InternalUpdate()
    {

    }
}
