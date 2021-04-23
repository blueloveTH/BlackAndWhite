using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] float startTime = 0f;
    [SerializeField] float interval = 2f;
    [SerializeField] Transform shootPoint;

    Collider2D selfCollider;

    // Start is called before the first frame update
    void Start()
    {
        selfCollider = GetComponent<Collider2D>();
        StartCoroutine(ShootCoroutine());
    }

    IEnumerator ShootCoroutine()
    {
        yield return new WaitForSeconds(startTime);
        while (true)
        {
            yield return new WaitForSeconds(interval);
            GameObject go = Instantiate(prefab, shootPoint.position, shootPoint.rotation);

            Physics2D.IgnoreCollision(selfCollider, go.GetComponent<Collider2D>());
        }
    }
}
