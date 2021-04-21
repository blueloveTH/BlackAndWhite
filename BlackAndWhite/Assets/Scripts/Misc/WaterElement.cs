using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFlow;

public class WaterElement : InteractiveBehaviour
{
    [SerializeField] AudioClip sfx;

    [SlotMethod("player_hit")]
    void OnSignal(Signal s)
    {
        s.source.GetCpntInOwner<PlayerBattleModel>().bubbleCount = 3;
        SFX.Play(sfx);
        Destroy(gameObject);
    }
}
