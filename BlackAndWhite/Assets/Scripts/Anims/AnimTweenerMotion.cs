using DG.Tweening;
using System.Collections;
using UnityEngine;

public class AnimTweenerMotion : AnimMotion
{
    [SerializeField] Ease normal = Ease.Linear;
    [SerializeField] Ease reverse = Ease.Linear;

    // Use this for initialization
    void Start()
    {
        Invoke("StartChange", startTime);
    }

    void StartChange()
    {
        StartCoroutine(_StartChange());
    }

    IEnumerator _StartChange()
    {
        while (true)
        {
            transform.DOMove(transform.position + (Vector3)targetVector * direction, duration)
                .SetEase<Tween>(direction > 0f ? normal : reverse);

            yield return new WaitForSeconds(interval + duration);
            direction *= -1;
            transform.localScale = transform.localScale * flipScale;
        }
    }

}
