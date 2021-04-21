using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : RigidbodyActor2D
{
    public float speed = 5;
    [SerializeField] SpriteRenderer spRenderer;
    [SerializeField] WeaponSlot weaponSlot;
    [SerializeField] Animator animator;

    [SerializeField] AudioSource stepSource;

    public static int Sign(float x)
    {
        if (Mathf.Approximately(x, 0))
            return 0;
        return (int)Mathf.Sign(x);
    }

    public void Move(float h, float v)
    {
        velocity = new Vector2(h, v) * speed;

        bool isWalking = velocity.sqrMagnitude > 1e-3;

        Vector2Int dir = Vector2Int.zero;
        if (isWalking)
        {
            dir.x = Sign(velocity.x);
            dir.y = Sign(velocity.y);

            if (Mathf.Abs(dir.x) >= Mathf.Abs(dir.y))
                dir.y = 0;
            else
                dir.x = 0;

            weaponSlot.transform.right = new Vector3(dir.x, dir.y);
        }

        animator.SetBool("isWalking", isWalking);
        animator.SetInteger("dir_x", dir.x);
        animator.SetInteger("dir_y", dir.y);

        stepSource.mute = !isWalking;
    }

    protected override void InternalUpdate()
    {
        animator.SetBool("isWalking", false);
        animator.SetInteger("dir_x", 0);
        animator.SetInteger("dir_y", 0);
    }
}
