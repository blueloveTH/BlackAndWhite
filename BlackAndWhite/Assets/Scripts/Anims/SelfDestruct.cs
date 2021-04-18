using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float delay = 4;

    private void Start()
    {
        Destroy(gameObject, delay);
    }
}

