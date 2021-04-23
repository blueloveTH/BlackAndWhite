using GameFlow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleModel : BattleModel
{
    public static PlayerBattleModel main { get; private set; }

    private void Awake()
    {
        main = this;
    }

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
        gameObject.SetActive(false);
        print("You failed!");

        Task.Delay(1f).OnComplete(() => Checkpoint.main.RenewScene()).Play();
    }

    int recordMana = 3;

    public void Record()
    {
        recordMana = bubbleCount;
    }

    public void Respawn()
    {
        gameObject.SetActive(true);
        HP = 1;
        transform.position = Checkpoint.main.position;
        bubbleCount = recordMana;
    }
}
