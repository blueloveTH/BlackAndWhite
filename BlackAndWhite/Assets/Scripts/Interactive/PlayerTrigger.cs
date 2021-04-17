using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFlow;

public class PlayerTrigger : InteractiveBehaviour
{
    public override GameObject owner => transform.parent.gameObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var playerHitSignal = Signal("player_hit");
        Emit(playerHitSignal, collision.gameObject);
    }
}
