using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFlow;
using UnityEngine.Events;

public class WaterElement : InteractiveBehaviour
{
    [SerializeField] AudioClip sfx;

    [SerializeField] UnityEvent onTrigger;

    [SlotMethod("player_hit")]
    void OnSignal(Signal s)
    {
        s.source.GetCpntInOwner<PlayerBattleModel>().bubbleCount = 3;
        SFX.Play(sfx);
        onTrigger.Invoke();
        Destroy(gameObject);
    }
}
