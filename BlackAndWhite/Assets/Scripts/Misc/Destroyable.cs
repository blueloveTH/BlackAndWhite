using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFlow;
using DG.Tweening;

public class Destroyable : InteractiveBehaviour
{
    public int HP = 1;
    [SerializeField] AudioClip sfx;

    [SerializeField] Sprite debrisSp;

    [SlotMethod("player_atk")]
    void OnSignal(Signal sig)
    {
        HP -= sig["ATK"];
        if (HP <= 0)
        {
            Task.Delay(0.16f).OnComplete(() => SFX.Play(sfx)).Play();

            if (debrisSp)
            {
                GetComponent<SpriteRenderer>().sprite = debrisSp;
                enabled = false;
                transform.DOKill();
                transform.rotation = Quaternion.identity;
                Destroy(GetComponent<AnimRotate>());
                Destroy(GetComponent<Collider2D>());
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

}
