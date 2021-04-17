using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
        int delta = parentGo.transform.childCount - value;
        if (delta > 0)
        {
            for (int i = 0; i < delta; i++)
                Destroy(parentGo.transform.GetChild(0).gameObject);
        }
        else
        {
            var prefab = parentGo.transform.GetChild(0).gameObject;
            for (int i = 0; i < -delta; i++)
            {
                Instantiate(prefab, parentGo.transform);
            }
        }
    }
}
