using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFlow;
using UnityEngine.Events;

public class CircleTeleport : InteractiveBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] UnityEvent onTeleport;

    [SlotMethod("player_hit")]
    void OnSignal(Signal sig)
    {
        sig.source.owner.transform.position = target.position;
        onTeleport.Invoke();
    }

}
