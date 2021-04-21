using GameFlow;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    [SerializeField] string trackName = "Main";
    [SerializeField] AudioClip clip;
    [SerializeField] [Range(0, 1)] float volume = 1;
    [SerializeField] float fadeTime = 1f;

    const Ease fadeOutEase = Ease.OutQuad;
    const Ease fadeInEase = Ease.InQuad;

    static Dictionary<string, TrackInfo> trackDic = new Dictionary<string, TrackInfo>();

    public static void DeleteTrack(string name)
    {
        if (!trackDic.ContainsKey(name)) return;
        TrackInfo ti = trackDic[name];
        ti.fadeTask?.Kill();
        Destroy(ti.a.gameObject);
        trackDic.Remove(ti.player.trackName);
    }

    /// <summary>
    /// Fadeout src and delete the track on complete
    /// </summary>
    public Task FadeoutTask(float duration)
    {
        if (track == null) return null;
        track.fadeTask?.Kill();
        var t = track.fadeTask = new TaskList()
        {
            track.a.DOFade(0, duration).SetEase(fadeOutEase),
        };

        t.onComplete += () => DeleteTrack(track.player.trackName);
        return t;
    }

    public static BGMPlayer FindTrack(string name)
    {
        if (trackDic.ContainsKey(name) == false) return null;
        return trackDic[name].player;
    }

    public static BGMPlayer Main { get { return FindTrack("Main"); } }

    private TrackInfo track { get; set; }

    class TrackInfo
    {
        public BGMPlayer player;
        public AudioSource a;
        public TaskList fadeTask;
        public AudioClip lastClip;
    }

    private void Awake()
    {
        if (!trackDic.ContainsKey(trackName))
        {
            track = new TrackInfo();
            track.a = SFX.GetInstance("BGM: " + trackName);
            track.a.volume = track.a.volume = 0;
            track.a.playOnAwake = track.a.playOnAwake = false;
            trackDic.Add(trackName, track);
        }
        else
        {
            track = trackDic[trackName];
        }

        track.player = this;
    }

    private void Start()
    {
        if (clip != null && clip != track.lastClip)
        {
            track.fadeTask?.Kill();
            track.lastClip = clip;

            track.fadeTask = new TaskList();
            if (track.a.volume >= 0.001f)
                track.fadeTask.Add(track.a.DOFade(0, fadeTime * 0.5f).SetEase(fadeOutEase));

            track.fadeTask.Add(() => track.a.PlayLoop(clip));
            track.fadeTask.Add(track.a.DOFade(volume, fadeTime * 0.5f).SetEase(fadeInEase));
            track.fadeTask.Play();
        }
    }
}
