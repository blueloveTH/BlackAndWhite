using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleModel : BattleModel
{
    public override int ATK => 1;

    public override int DEF => 0;


    public override void OnDeath()
    {
        ObjectFX.Destroy(gameObject);
        Debug.Log("You failed!");
    }
}
