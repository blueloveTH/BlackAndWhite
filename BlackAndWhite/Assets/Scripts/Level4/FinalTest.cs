using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFlow;
using DG.Tweening;

public class FinalTest : InteractiveBehaviour
{
    [SerializeField] SpriteRenderer maskRenderer;

    private void Start()
    {
        maskRenderer.DOFade(0, 1f);
    }
}
