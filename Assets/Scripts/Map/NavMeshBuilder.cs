using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBuilder : MonoBehaviour {

    public Player player;

    void Start () {

        NavMeshSurface[] surfaces = GameObject.FindObjectsOfType<NavMeshSurface>();
        for (int i = 0; i < surfaces.Length; i++)
        {
            surfaces[i].BuildNavMesh();
        }
        GameObject.Instantiate(player, Vector3.right*48, Quaternion.identity);
	}
	
}
