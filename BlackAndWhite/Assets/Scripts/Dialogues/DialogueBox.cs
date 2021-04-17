using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GameFlow;

public class DialogueBox : MonoBehaviour
{
    public static DialogueBox main { get; private set; }
    private CanvasGroup cgGroup;

    private void Awake()
    {
        main = this;
        cgGroup = GetComponent<CanvasGroup>();
    }

    [SerializeField] TextMeshProUGUI txt;

    private Task displayTask;

    public Task DisplayInfo(List<string> list)
    {
        cgGroup.alpha = 1;
        cgGroup.blocksRaycasts = true;

        displayTask = new TaskList();
        StartCoroutine(DisplayLoop(list));
        return displayTask;
    }

    bool CanContinue()
    {
        return Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0);
    }

    //bool CanSkip()
    //{
    //   return Input.GetKeyDown(KeyCode.Escape);
    //}

    IEnumerator DisplayLoop(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            txt.text = list[i];
            yield return new WaitUntil(CanContinue);
            yield return new WaitForEndOfFrame();
        }

        cgGroup.alpha = 0;
        cgGroup.blocksRaycasts = false;

        displayTask.Play();
    }
}
