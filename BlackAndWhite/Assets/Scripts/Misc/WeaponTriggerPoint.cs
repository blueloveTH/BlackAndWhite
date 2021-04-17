using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFlow;

public class WeaponTriggerPoint : InteractiveBehaviour
{
    [SerializeField] float radius = 0.5f;
    [SerializeField] LayerMask layers = -1;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void SignalTarget()
    {
        Collider2D c2d = Physics2D.OverlapCircle(transform.position, radius, layers);
        if (c2d != null)
        {
            var sig = Signal("player_atk");
            sig["ATK"] = 1;
            Emit(sig, c2d.gameObject);
        }
        else
        {
            print("Nothing.");
        }
    }
}
