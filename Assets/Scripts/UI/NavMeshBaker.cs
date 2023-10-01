using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;


namespace DecayingMarine
{
    public class NavMeshBaker : MonoBehaviour
    {
        public void BuildNavMesh()
        {
            var surfaces = FindObjectsOfType<NavMeshSurface>();
            for(int i = 0; i < surfaces.Length; i++)
            {
                surfaces[i].BuildNavMesh();
            }
        }
    }
}
