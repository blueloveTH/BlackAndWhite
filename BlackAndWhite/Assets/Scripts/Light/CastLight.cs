using UnityEngine;

public class CastLight : MonoBehaviour
{
    private ZippyLights2D light2D;

    // Start is called before the first frame update
    void Start()
    {
        light2D = GetComponentInChildren<ZippyLights2D>();

        light2D.observations.OnAdd += Observations_OnAdd;
        light2D.observations.OnRemove += Observations_OnRemove;
    }

    private void Observations_OnRemove(GameObject obj)
    {
        Lightness lightness;
        if(obj.TryGetComponent(out lightness))
        {
            lightness.lightCount--;
        }
    }

    private void Observations_OnAdd(GameObject obj)
    {
        Lightness lightness;
        if (obj.TryGetComponent(out lightness))
        {
            lightness.lightCount++;
        }
    }
}
