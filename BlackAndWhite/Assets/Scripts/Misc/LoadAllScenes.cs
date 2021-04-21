using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-500)]
public class LoadAllScenes : MonoBehaviour
{
    [SerializeField] List<string> scenes;

    // Start is called before the first
    private void Awake()
    {
#if !UNITY_EDITOR
        foreach (var item in scenes)
        {
            SceneManager.LoadScene(item, LoadSceneMode.Additive);
        }
#endif
    }
}
