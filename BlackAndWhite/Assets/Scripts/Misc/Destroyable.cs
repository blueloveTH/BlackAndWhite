using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFlow;

public class Destroyable : InteractiveBehaviour
{
    public int HP = 1;
    [SerializeField] AudioClip sfx;

    [SlotMethod("player_atk")]
    void OnSignal(Signal sig)
    {
        HP -= sig["ATK"];
        if (HP <= 0)
        {
            gameObject.SetActive(false);
            SFX.Play(sfx);
        }
    }

}
