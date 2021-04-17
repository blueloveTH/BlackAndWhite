using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFlow;
using DG.Tweening;
using UnityEngine.Playables;

public class WeaponSlot : MonoBehaviour
{
    public WeaponTriggerPoint wtp;

    public bool isReady = true;

    public void Attack()
    {
        if (wtp == null || !isReady) return;
        isReady = false;

        // do attack here
        print("Attack!");
        wtp.SignalTarget();

        Task.Delay(0.75f).OnComplete(() => isReady = true).Play();
    }
}
