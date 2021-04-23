using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerDownHandler
{
    Color initialColor;

    [SerializeField] AudioClip sfx;

    TextMeshProUGUI txt;

    public void OnPointerDown(PointerEventData eventData)
    {
        SceneManager.LoadSceneAsync("SampleScene");
        txt.raycastTarget = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        txt.color = Color.white;
        SFX.Play(sfx, 0.1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        txt.color = initialColor;
    }

    private void Awake()
    {
        txt = GetComponent<TextMeshProUGUI>();
        initialColor = txt.color;
    }

}
