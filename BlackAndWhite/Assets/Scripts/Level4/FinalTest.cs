using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFlow;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class FinalTest : InteractiveBehaviour
{
    [SerializeField] SpriteRenderer maskRenderer;
    [SerializeField] CircleTeleport circleTeleport;

    private int btnCount = 0;

    [SlotMethod("player_hit")]
    private void OnSignal(Signal sig)
    {
        maskRenderer.DOFade(0, 1f);
        transform.GetChild(0).gameObject.SetActive(true);
        enabled = false;
    }

    public void AddBtnCount()
    {
        btnCount++;
        if (btnCount == 3)
            circleTeleport.Unlock();
    }

    public void Win()
    {
        Camera.main.GetComponent<CameraFollow>().enabled = false;

        var img = GameObject.Find("Canvas/UI Mask").GetComponent<UnityEngine.UI.Image>();
        img.DOFade(1, 1f).SetEase(Ease.OutQuad).OnComplete(
            () => SceneManager.LoadScene("The End")
            );
    }
}
