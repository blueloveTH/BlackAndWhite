using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GameFlow;

public class DialogueBox : MonoBehaviour
{
    public static DialogueBox main { get; private set; }
    private CanvasGroup cgGroup;

    [SerializeField] AudioClip textTrFx;

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
        return Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);
    }

    //bool CanSkip()
    //{
    //   return Input.GetKeyDown(KeyCode.Escape);
    //}

    IEnumerator DisplayLoop(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            SFX.Play(textTrFx, 0.3f);
            txt.text = list[i];
            yield return new WaitUntil(CanContinue);
            yield return new WaitForSeconds(0.1f);
        }

        cgGroup.alpha = 0;
        cgGroup.blocksRaycasts = false;

        displayTask.Play();
    }
}
