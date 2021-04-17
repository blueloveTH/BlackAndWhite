using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator4 : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spRenderer;

    [SerializeField] Sprite leftIdle, rightIdle, upIdle, downIdle;

    public void CustomUpdate(bool isWalking, Vector2Int dir)
    {
        if (!isWalking)
        {
            spRenderer.sprite = leftIdle;
        }


    }
}
