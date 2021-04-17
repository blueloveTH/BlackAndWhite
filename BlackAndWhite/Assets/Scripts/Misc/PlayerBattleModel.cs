using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleModel : BattleModel
{
    public int _bubbleCount = 3;

    public event System.Action<int> onBubbleChange;

    public int bubbleCount
    {
        get => _bubbleCount;
        set
        {
            if (_bubbleCount == value) return;
            onBubbleChange?.Invoke(value);
            _bubbleCount = value;
        }
    }

    public override int ATK => 1;

    public override int DEF => 0;


    public override void OnDeath()
    {
        ObjectFX.Destroy(gameObject);
        Debug.Log("You failed!");
    }
}
