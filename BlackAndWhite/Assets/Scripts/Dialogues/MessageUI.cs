using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GameFlow;

public class MessageUI : MonoBehaviour
{
    public static MessageUI main { get; private set; }
    private TextMeshProUGUI tmpPro;
    private Task clearTask;

    private void Awake()
    {
        main = this;
        tmpPro = GetComponent<TextMeshProUGUI>();
    }

    public void Display(string txt, float duration = 2.5f)
    {
        tmpPro.text = txt;
        clearTask?.Kill();
        clearTask = Task.Delay(duration).OnComplete(() => tmpPro.text = "");
        clearTask.Play();
    }
}
