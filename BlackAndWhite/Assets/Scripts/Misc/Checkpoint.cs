using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFlow;
using UnityEngine.SceneManagement;

public class Checkpoint : InteractiveBehaviour
{
    public static Checkpoint main { get; private set; }
    [SerializeField] string sceneName;
    [SerializeField] List<string> unloadScenes;

    public Vector3 position => transform.position;

    [SlotMethod("player_hit")]
    void OnSignal(Signal sig)
    {
        if (main == this) return;

        foreach (var s in unloadScenes)
            SceneManager.UnloadSceneAsync(s);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);

        main = this;

        MessageUI.main.Display("Checkpoint updated.");
        PlayerBattleModel.main.Record();

        enabled = false;
    }

    public void RenewScene()
    {
        SceneManager.UnloadSceneAsync(sceneName);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }
}
