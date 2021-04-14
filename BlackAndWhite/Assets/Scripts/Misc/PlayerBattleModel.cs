using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class PlayerBattleModel : BattleModel
{
    public Weapon weapon;

    public override int ATK => weapon.ATK;

    public override int DEF => 0;


    public override void OnDeath()
    {
        ObjectFX.Destroy(gameObject);
        Debug.Log("You failed!");
    }
}
