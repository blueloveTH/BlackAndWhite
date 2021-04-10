using UnityEditor;
using UnityEngine;

namespace FogOfWar
{
    internal class CreateFOW : MonoBehaviour
    {
        [MenuItem("FogOfWar/CreateSystem")]
        private static void NewFOWSystem()
        {
            Object prefabs = Resources.Load("FOWSystem");
            if (prefabs != null)
            {
                GameObject go = Instantiate(prefabs) as GameObject;
                go.name = "FOWSystem";
            }
        }

        [MenuItem("FogOfWar/CreateSystem2D")]
        private static void NewFOWSystem2D()
        {
            Object prefabs = Resources.Load("FOWSystem2D");
            if (prefabs != null)
            {
                GameObject go = Instantiate(prefabs) as GameObject;
                go.name = "FOWSystem2D";
            }
        }
    }
}