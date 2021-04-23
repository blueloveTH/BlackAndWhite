using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnStart : MonoBehaviour
{
    [SerializeField] Object cpnt;

    // Start is called before the first frame update
    void Start()
    {
        dynamic cpnt_ = cpnt;
        cpnt_.enabled = true;
    }

}
