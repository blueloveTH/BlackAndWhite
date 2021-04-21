using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;


public class ChargeProgress : MonoBehaviour
{
    [SerializeField] Image img;
    [SerializeField] TextMeshProUGUI text;

    [SerializeField] bool displayText = true;

    CanvasGroup cgGroup;

    private void Awake()
    {
        cgGroup = GetComponent<CanvasGroup>();
    }

    public void SetValue(float t)
    {
        if (t == 0)
        {
            cgGroup.alpha = 0;
            return;
        }

        cgGroup.alpha = 1;

        img.fillAmount = t;
        if (displayText)
            text.text = Mathf.RoundToInt(t * 100).ToString() + "%";
    }
}
