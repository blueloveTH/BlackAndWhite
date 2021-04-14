using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFlow;
using DG.Tweening;
using UnityEngine.Playables;

public class WeaponSlot : MonoBehaviour
{
    public Weapon weapon;

    private PlayableDirector director;


    private void Awake()
    {
        director = GetComponent<PlayableDirector>();
    }

    public bool isReady = true;

    public void Attack()
    {
        if (weapon == null || !isReady) return;

        // do attack here
        print("Attack!");

        director.Play();

        Task.Delay(0.75f).OnComplete(() => isReady = true).Play();
    }
}
