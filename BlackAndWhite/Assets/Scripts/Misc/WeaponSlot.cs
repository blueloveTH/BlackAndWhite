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
    PlayerBattleModel model;

    private void Awake()
    {
        model = GetComponentInParent<PlayerBattleModel>();
    }

    public bool CanAttack()
    {
        if (model.bubbleCount == 0) return false;
        if (wtp == null) return false;
        return true;
    }

    public void Attack()
    {
        if(wtp.SignalTarget()) model.bubbleCount -= 1;
    }
}
