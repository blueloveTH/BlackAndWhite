using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public static Checkpoint main { get; private set; }

    public Vector3 position => transform.position;

    private void OnEnable()
    {
        main = this;
    }
}
