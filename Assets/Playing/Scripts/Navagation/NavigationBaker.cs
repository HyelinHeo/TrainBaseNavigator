using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationBaker : MonoBehaviour
{
    public NavMeshSurface[] surfaces;

    void Start()
    {

    }

    public void Bake()
    {
        surfaces = gameObject.GetComponentsInChildren<NavMeshSurface>();

        if (surfaces != null)
        {
            for (int i = 0; i < surfaces.Length; i++)
            {
                Debug.Log("bake: " + surfaces[i]);
                surfaces[i].layerMask = 1 << LayerMask.NameToLayer("FoundationObject");
                surfaces[i].BuildNavMesh();

            }
        }
    }
}
