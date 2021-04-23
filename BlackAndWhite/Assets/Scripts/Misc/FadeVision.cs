using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FadeVision : MonoBehaviour
{
    float duration = 0.8f;
    [SerializeField] Sprite circleSp;

    public void DetachAndFade()
    {
        transform.SetParent(null, true);
        var spRenderer = gameObject.AddComponent<SpriteRenderer>();

        Destroy(GetComponent<AnimScaling>());

        transform.DOScale(0f, duration).SetEase(Ease.InExpo).OnComplete(() => Destroy(gameObject));
        /*
        var globalMask = GetComponent<SpriteMask>();
        globalMask.isCustomRangeActive = true;

        spRenderer.sprite = circleSp;
        spRenderer.sortingLayerName = "Effect";
        spRenderer.sortingOrder = 1;
        spRenderer.color = new Color(0, 0, 0, 0);
        spRenderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        spRenderer.DOFade(1, duration).OnComplete(() => Destroy(gameObject));*/
    }
}
