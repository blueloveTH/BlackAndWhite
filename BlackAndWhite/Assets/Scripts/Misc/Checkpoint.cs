using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFlow;
using UnityEngine.SceneManagement;
using DG.Tweening;

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
        var img = GameObject.Find("Canvas/UI Mask").GetComponent<UnityEngine.UI.Image>();
        var seq = DOTween.Sequence();
        seq.Append(img.DOFade(1, 0.25f).SetEase(Ease.OutQuad));
        seq.AppendCallback(() =>
        {
            PlayerBattleModel.main.Respawn();
            //Camera.main.GetComponent<CameraFollow>().Align();
            SceneManager.UnloadSceneAsync(sceneName);
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        });
        seq.Append(img.DOFade(0, 0.25f).SetEase(Ease.InQuad));
        seq.Play();
    }
}
