using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightness : MonoBehaviour
{
    private int _lightCount = 0;

    public event System.Action OnLightChange;


    public int lightCount
    {
        get { return _lightCount; }
        set
        {
            if (value == _lightCount)
                return;
            _lightCount = value;
            OnLightChange?.Invoke();
        }
    }
}
