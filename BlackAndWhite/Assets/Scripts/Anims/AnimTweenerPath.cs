using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Plugins.Core.PathCore;

public class AnimTweenerPath : MonoBehaviour
{
    [SerializeField] List<Transform> wayTransforms;
    [SerializeField] float duration = 3f;

    public Vector3[] waypoints => wayTransforms.ConvertAll((x) => x.position).ToArray();

    private void Start()
    {
        Path path = new Path(PathType.Linear, waypoints, 16);
        var t = transform.DOPath(path, duration, PathMode.TopDown2D);
        t.SetLoops(-1).SetEase(Ease.Linear);
    }

    void OnDestroy()
    {
        transform.DOKill();
    }
}
