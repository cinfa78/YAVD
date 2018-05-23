using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBuilder : MonoBehaviour {

    void Start () {
        CreateLevelNavmesh();
	}

	public void CreateLevelNavmesh()
    {
        NavMeshSurface surface = GameObject.FindObjectOfType<NavMeshSurface>();
        if (surface)
        {
            surface.BuildNavMesh();
        }
    }

}
