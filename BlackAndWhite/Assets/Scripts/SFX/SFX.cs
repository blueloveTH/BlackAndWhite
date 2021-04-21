using GameFlow;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A shorthand to play 2d audios
/// </summary>
public sealed class SFX : MonoBehaviour
{
    private static SFX _main;
    private static SFX Main {
        get {
            if (_main == null)
            {
                GameObject go = new GameObject("SFX Player");
                _main = go.AddComponent<SFX>();
                DontDestroyOnLoad(go);
            }
            return _main;
        }
    }

    private static AudioSource CreateBuffer()
    {
        GameObject obj = new GameObject();
        obj.transform.parent = Main.transform;
        AudioSource src = obj.AddComponent<AudioSource>();
        return src;
    }
    private static void OnPushBuffer(AudioSource src) { src.gameObject.SetActive(false); }
    private static void OnPopBuffer(AudioSource src) { src.gameObject.SetActive(true); }

    private ObjectPool<AudioSource> bufferList = new ObjectPool<AudioSource>(CreateBuffer, OnPushBuffer, OnPopBuffer);
    public static void Play(AudioClip clip, Vector3 worldPos, float vol = 1f) { Main._PlaySound(clip, worldPos, vol); }
    public static void Play(AudioClip clip, float vol = 1f) { Main._PlaySound(clip, Vector3.zero, vol); }
    private void _PlaySound(AudioClip clip, Vector3 worldPos, float vol)
    {
        if (clip == null) return;
        AudioSource ac = bufferList.Pop();
        ac.transform.position = worldPos;
        ac.volume = vol;
        ac.PlayOneShot(clip);
        playList.Add(ac);
    }

    internal static AudioSource GetInstance(string name)
    {
        AudioSource src = CreateBuffer();
        src.name = name;
        return src;
    }

    private List<AudioSource> playList = new List<AudioSource>();

    private void LateUpdate()
    {
        for (int i = playList.Count - 1; i >= 0; i--)
        {
            if (playList[i].isPlaying == false)
            {
                bufferList.Push(playList[i]);
                playList.RemoveAt(i);
            }
        }
    }
}

public static class AudioEx
{
    public static void PlayLoop(this AudioSource src, AudioClip clip)
    {
        src.Stop();
        src.clip = clip;
        if (!src.loop) src.loop = true;
        src.Play();
    }
}
