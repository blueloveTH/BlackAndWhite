using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerViewer : MonoBehaviour
{
    PlayerBattleModel model;
    [SerializeField] GameObject parentGo;

    private void Awake()
    {
        model = GetComponentInParent<PlayerBattleModel>();
        model.onBubbleChange += Model_onBubbleChange;
    }

    private void Model_onBubbleChange(int value)
    {
        Color colorEnabled = new Color(1, 1, 1, 1f);
        Color colorDisabled = new Color(1, 1, 1, 0.3f);

        for (int i = 0; i < value; i++)
            parentGo.transform.GetChild(i).GetComponent<Image>().color = colorEnabled;
        for(int i = value; i < 3; i++)
            parentGo.transform.GetChild(i).GetComponent<Image>().color = colorDisabled;
    }
}
