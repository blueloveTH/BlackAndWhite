using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFlow;

public class DialogueTrigger : InteractiveBehaviour
{
    [SerializeField] List<string> dialogues = new List<string>();

    [SlotMethod("player_hit")]
    void OnSignal(Signal s)
    {
        PlayerController.main.enabled = false;
        var task = DialogueBox.main.DisplayInfo(dialogues);
        task.onComplete += () => PlayerController.main.enabled = true;
    }
}
