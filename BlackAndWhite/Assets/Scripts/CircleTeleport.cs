using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFlow;
using UnityEngine.Events;

public class CircleTeleport : InteractiveBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] string targetPath;
    [SerializeField] UnityEvent onTeleport;

    [SerializeField] AudioClip sfx;

    [SerializeField] bool _isLocked = false;

    public bool isLocked
    {
        get => _isLocked;
        set
        {
            _isLocked = value;
            transform.Find("LOCK").gameObject.SetActive(value);
        }
    }

    public void Unlock()
    {
        if (!isLocked) return;
        isLocked = false;
        MessageUI.main.Display("The portal is unlocked.");
    }

    private void Awake()
    {
        if (_isLocked)
            transform.Find("LOCK").gameObject.SetActive(true);
    }

    [SlotMethod("player_hit")]
    void OnSignal(Signal sig)
    {
        if (_isLocked)
        {
            MessageUI.main.Display("The portal is locked.");
            return;
        }
        if (PlayerBattleModel.main.bubbleCount < 1)
        {
            MessageUI.main.Display("You need at least 1 bubble to teleport.");
            return;
        }


        SFX.Play(sfx);
        PlayerBattleModel.main.bubbleCount -= 1;

        Vector3 targetPos = Vector3.zero;
        if (target != null)
            targetPos = target.position;
        if (!string.IsNullOrEmpty(targetPath))
            targetPos = GameObject.Find(targetPath).transform.position;

        sig.source.owner.transform.position = targetPos;
        onTeleport.Invoke();
    }

}
