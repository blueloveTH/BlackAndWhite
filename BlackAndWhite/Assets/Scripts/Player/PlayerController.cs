using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFlow;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController main { get; private set; }

    [SerializeField] PlayerActor actor;
    [SerializeField] WeaponSlot weaponSlot;

    [SerializeField] AudioClip chargeFx;

    private void Start()
    {
        main = this;
    }

    private void OnEnable()
    {
        actor.OnUpdate += Actor_OnUpdate;
    }

    private void OnDisable()
    {
        actor.OnUpdate -= Actor_OnUpdate;
        actor.Move(0, 0);
        delayTask?.Kill();
    }

    private void Actor_OnUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (delayTask.IsPlaying())
        {
            h = v = 0;
        }
            

        actor.Move(h, v);

        HandleATK();
    }

    private ProgressDelayTask delayTask;
    [SerializeField] ChargeProgress chargeProgress;

    private void HandleATK()
    {
        if (delayTask.IsPlaying())
        {
            if (Input.GetKeyUp(KeyCode.K))
                delayTask.Kill();
            return;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            if (weaponSlot.CanAttack())
            {
                delayTask = Task.ProgressDelay(0.8f);
                delayTask.onComplete += () =>
                {
                    chargeProgress.SetValue(0);
                    weaponSlot.Attack();
                };

                delayTask.onPlay += () => SFX.Play(chargeFx);
                delayTask.onKill += () => chargeProgress.SetValue(0);
                delayTask.onUpdate += (x) => chargeProgress.SetValue(x);
                delayTask.Play();
            }
        }
    }
}
