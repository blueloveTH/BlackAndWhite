using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFlow;

public class DeathTrigger : InteractiveBehaviour
{
    [SerializeField] AudioClip sfx;

    [SlotMethod("player_hit")]
    void OnSignal(Signal signal)
    {
        SFX.Play(sfx, 1f);
        var battleModel = signal.source.GetCpntInOwner<PlayerBattleModel>();
        battleModel.HP -= 100;
    }
}
