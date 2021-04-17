using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFlow;
using DG.Tweening;
using UnityEngine.Playables;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
{
    public WeaponTriggerPoint wtp;
    [SerializeField] Image iconImage;

    public bool isReady = true;

    public void Attack()
    {
        if (wtp == null || !isReady) return;
        isReady = false;

        // do attack here
        print("Attack!");
        wtp.SignalTarget();

        var t = Task.ProgressDelay(0.75f);
        t.onComplete += () => isReady = true;
        t.onUpdate += (x) => iconImage.fillAmount = x;
        t.Play();
    }
}
