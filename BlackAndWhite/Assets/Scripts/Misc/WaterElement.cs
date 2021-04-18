using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFlow;

public class WaterElement : InteractiveBehaviour
{
    [SlotMethod("player_hit")]
    void OnSignal(Signal s)
    {
        s.source.GetCpntInOwner<PlayerBattleModel>().bubbleCount = 3;
        Destroy(gameObject);
    }
}
