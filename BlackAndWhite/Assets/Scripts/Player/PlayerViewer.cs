using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerViewer : MonoBehaviour
{
    [Header("Lightness")]
    [SerializeField] Lightness lightness;
    [SerializeField] TextMeshProUGUI lightnessTextMesh;

    private void Awake()
    {
        lightness.OnLightChange += Lightness_OnLightChange;
    }

    private void Lightness_OnLightChange()
    {
        lightnessTextMesh.text = string.Format("Light: {0}", lightness.lightCount);
    }
}
