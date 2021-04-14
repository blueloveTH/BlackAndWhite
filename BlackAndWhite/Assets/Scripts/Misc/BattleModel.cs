using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleModel : MonoBehaviour
{
    private int _hp = 1;

    public int HP
    {
        get { return _hp; }
        set
        {
            _hp = value;
            if (_hp < 0)
            {
                _hp = 0;
                OnDeath();
            }
        }
    }


    public virtual int ATK { get; }
    public virtual int DEF { get; }

    public abstract void OnDeath();
}
