using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Plugins.Core.PathCore;

public class AIGear : MonoBehaviour
{
    [SerializeField] List<Transform> wayTransforms;
    [SerializeField] float speed = 6.4f;

    public Vector3[] waypoints;

    [SerializeField] Transform leftEnd, rightEnd;

    private void Start()
    {
        waypoints = wayTransforms.ConvertAll((x) => x.position).ToArray();
        Path path = new Path(PathType.Linear, waypoints, 16);

        float totalLength = 0;
        for (int i = 1; i < waypoints.Length; i++)
            totalLength += Vector2.Distance(waypoints[i - 1], waypoints[i]);

        float duration = totalLength / speed;
        var t = transform.DOPath(path, duration, PathMode.TopDown2D);
        t.SetEase(Ease.Linear);
        t.onComplete += FinalRoute;
    }

    private void FinalRoute()
    {
        float player_x = PlayerBattleModel.main.transform.position.x;
        if (transform.position.x < player_x)
        {
            //right
            var dis = Vector2.Distance(rightEnd.position, transform.position);
            transform.DOMove(rightEnd.position, dis / speed).SetEase(Ease.Linear);
        }
        else
        {
            //left
            var dis = Vector2.Distance(leftEnd.position, transform.position);
            transform.DOMove(leftEnd.position, dis / speed).SetEase(Ease.Linear);
        }
    }

    void OnDestroy()
    {
        transform.DOKill();
    }
}
