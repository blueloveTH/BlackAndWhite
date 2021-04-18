using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFlow;
using UnityEngine.Events;

public class CircleTeleport : InteractiveBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] UnityEvent onTeleport;

    [SerializeField] bool _isLocked = false;

    public bool isLocked
    {
        get => _isLocked;
        set{
            _isLocked = value;
            transform.Find("LOCK").gameObject.SetActive(value);
        }
    }

    public void Unlock()
    {
        isLocked = false;
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
            MessageUI.main.Display("The circle is locked.");
            return;
        }
        if (PlayerBattleModel.main.bubbleCount < 1)
        {
            MessageUI.main.Display("You need at least 1 mana to teleport.");
            return;
        }
            

        PlayerBattleModel.main.bubbleCount -= 1;

        sig.source.owner.transform.position = target.position;
        onTeleport.Invoke();
    }

}
