/*
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using BL.Kernel;

[RequireComponent(typeof(Text))]
[DisallowMultipleComponent()]
public class TextDisplayer : MonoBehaviour
{
    private Text _textCpnt;
    protected Text textCpnt {
        get {
            if (_textCpnt == null) _textCpnt = GetComponent<Text>();
            return _textCpnt;
        }
    }
    [SerializeField] bool translatable = false;

    public const char NO_BREAKING_SPACE = '\u00A0';

    public Color color { get { return textCpnt.color; } set { textCpnt.color = value; } }
    public int fontSize { get { return textCpnt.fontSize; } set { textCpnt.fontSize = value; } }
    protected virtual void Awake()
    {
        UpdateLanguage("zh_cn");
        Lang.OnTargetChange += UpdateLanguage;
    }

    protected virtual void OnDestroy()
    {
        Lang.OnTargetChange -= UpdateLanguage;
    }

    public void UpdateLanguage(string target)
    {
        if (translatable) textCpnt.text = Lang.Tr(textCpnt.text, target);
    }

    public string text {
        get {
            return textCpnt.text;
        }
        set {
            textCpnt.text = value;
        }
    }
    public void SetText(string text)
    {
        if (translatable) textCpnt.text = Lang.Tr(text);
        else textCpnt.text = text;
    }
    public void SetText(string text, Color c)
    {
        color = c;
        SetText(text);
    }

    public void SetInt(int x) { text = x.ToString(); }
    public void SetSlash(int x, int y) { SetSlash(new Vector2Int(x, y)); }
    public void SetSlash(Vector2Int v) { text = v.ToSlash(); }
    public void SetNativeSize() { textCpnt.SetNativeSize(); }
    public void SetPercentage(int value)
    {
        value = Mathf.Clamp(value, 0, 100);
        text = value.ToString().PadLeft(3, ' ') + "%";
    }
    public void SetPercentage(float value01)
    {
        SetPercentage(Mathf.RoundToInt(value01 * 100f));
    }

    private DisplayTask displayTask;

    [System.Obsolete("Use NewDisplayTask() instead.")]
    public DisplayTask DisplayOneByOneTask(string fullText, int startIndex = 0, int speed = 12, bool stopPrevious = false)
    {
        return NewDisplayTask(fullText, startIndex, speed, stopPrevious);
    }

    public DisplayTask NewDisplayTask(string fullText, int startIndex = 0, int speed = 12, bool stopPrevious = false)
    {
        if (TaskUtil.IsPlaying(displayTask))
        {
            if (stopPrevious) displayTask.Kill();
            else return null;
        }

        DisplayTask task = new DisplayTask(this);
        task.SetParameters(fullText, startIndex, speed);
        return task;
    }

    public Task DOFadeTask(float endAlpha, float duration)
    {
        return new TweeningTask(textCpnt.DOFade(endAlpha, duration));
    }
    public void DOFadeToZero(float duration)
    {
        DOFadeTask(0, duration).Play();
    }
    public void DOFadeToOne(float duration)
    {
        DOFadeTask(1, duration).Play();
    }

    public class DisplayTask : MonoTask<TextDisplayer>
    {
        private string fullText = string.Empty;
        private int startIndex;
        private int speed;

        public DisplayTask(TextDisplayer displayer) : base(displayer) { }

        public void SetParameters(string fullText, int startIndex, int speed)
        {
            this.fullText = fullText;
            this.startIndex = startIndex;
            this.speed = speed;
        }

        public override void Play()
        {
            base.Play();
            StartAutoCoroutine(DisplayCoroutine());
        }

        private IEnumerator DisplayCoroutine()
        {
            //text = text.Replace(' ', NO_BREAKING_SPACE);
            owner.text = fullText.Substring(0, startIndex);
            fullText = fullText.Substring(startIndex);

            MatchCollection mc = Regex.Matches(fullText, "(<color=#[A-Z0-9]{6}>).*(</color>)");
            Dictionary<int, Match> matches = new Dictionary<int, Match>(mc.Count);

            for (int i = 0; i < mc.Count; i++)
                matches.Add(mc[i].Index, mc[i]);

            for (int i = 0; i < fullText.Length; i++)
            {
                yield return new WaitForSecondsRealtime(1f / speed);

                if (matches.ContainsKey(i))
                {
                    Match m = matches[i];
                    owner.text += m.Groups[1].Value + m.Groups[2].Value;
                    i = i + m.Groups[1].Length;

                    for (; i < m.Groups[2].Index; i++)
                    {
                        yield return new WaitForSecondsRealtime(1f / speed);
                        owner.text = owner.text.Insert(i, fullText[i].ToString());
                    }

                    i = i + m.Groups[2].Length - 1;
                    continue;
                }
                owner.text += fullText[i];
            }
            Complete();
        }
    }
}
*/