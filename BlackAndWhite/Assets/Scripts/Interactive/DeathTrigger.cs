using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFlow;

public class DeathTrigger : InteractiveBehaviour
{
    [SlotMethod("player_hit")]
    void OnSignal(Signal signal)
    {
        var battleModel = signal.source.GetCpntInOwner<PlayerBattleModel>();
        battleModel.HP -= 100;
    }
}
