using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFlow;

public class FireballTrigger : InteractiveBehaviour
{
    private void Awake()
    {
        Destroy(gameObject, 8f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

    [SlotMethod("player_hit")]
    void OnPlayerHit(Signal sig) { }
}
