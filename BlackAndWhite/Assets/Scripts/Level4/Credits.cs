using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Credits : MonoBehaviour
{
    [SerializeField] float speed = 100;

    private void Start()
    {
        /*
        var txts = GetComponentsInChildren<TextMeshProUGUI>();
        string s = "";
        foreach (var item in txts)
        {
            s += item.text + "\n";
        }
        print(s);*/
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
        if (transform.localPosition.y > 1400)
            transform.localPosition = new Vector3(0, -285, 0);

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}
