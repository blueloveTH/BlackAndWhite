using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFlow;
using UnityEngine.SceneManagement;

public class Checkpoint : InteractiveBehaviour
{
    public static Checkpoint main { get; private set; }
    [SerializeField] string sceneName;


    public Vector3 position => transform.position;

    [SlotMethod("player_hit")]
    void OnSignal(Signal s)
    {
        if (main == this) return;

        main = this;
        MessageUI.main.Display("Checkpoint updated.");
        PlayerBattleModel.main.Record();
    }

    public void RenewScene()
    {
        SceneManager.UnloadSceneAsync(sceneName);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }
}
