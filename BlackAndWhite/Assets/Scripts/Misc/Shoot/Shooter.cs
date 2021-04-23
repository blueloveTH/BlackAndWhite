using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] float interval = 2f;
    [SerializeField] Transform shootPoint;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootCoroutine());
    }

    IEnumerator ShootCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            GameObject go = Instantiate(prefab, shootPoint.position, shootPoint.rotation);
        }
    }
}
