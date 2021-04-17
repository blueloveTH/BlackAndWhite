using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFlow;

public class GetDamage : InteractiveBehaviour
{
    public int HP = 1;

    [SlotMethod("player_atk")]
    void OnSignal(Signal sig)
    {
        HP -= sig["ATK"];
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }

}
