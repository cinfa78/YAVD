using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshSurface))]
public class RoomManager : MonoBehaviour
{

    //temporaneo
    public GameObject roomPrefabDefault;
    GameObject[] spawnPlayer;
    GameObject[] spawnMonster;
    Door[] doors;
    Player[] players;
    Monster[] monsters;

    NavMeshSurface surface;

    void Awake()
    {
        surface = GetComponent<NavMeshSurface>();
        players = GameObject.FindObjectsOfType<Player>();
        foreach (Player p in players)
        {
            p.EnableAgent(false);
        }
    }
    void Start()
    {
        if (roomPrefabDefault)
            InitRoom(roomPrefabDefault);
    }

    public void InitRoom(GameObject roomPrefab)
    {
        GameObject room = Instantiate(roomPrefab);
        surface.BuildNavMesh();
        doors = GameObject.FindObjectsOfType<Door>();
        spawnMonster = GameObject.FindGameObjectsWithTag("Spawner");
        spawnPlayer = GameObject.FindGameObjectsWithTag("SpawnerPlayer");
        int i = 0;
        foreach (Player p in players)
        {
            p.transform.position = spawnPlayer[i % spawnPlayer.Length].transform.position;
            p.transform.rotation = spawnPlayer[i % spawnPlayer.Length].transform.rotation;
            print(p.transform.position + " " + spawnPlayer[i % spawnPlayer.Length].transform.position);
            float minDistance = 80000f;
            int closestDoor = 0;
            for (int j = 0; j < doors.Length; j++)
            {
                if (Vector3.Distance(p.transform.position, doors[j].transform.position) < minDistance)
                {
                    minDistance = Vector3.Distance(p.transform.position, doors[j].transform.position);
                    closestDoor = j;
                }
            }
            doors[closestDoor].Block();
            i++;
            p.EnableAgent(true);
        }
        monsters = GameObject.FindObjectsOfType<Monster>();
        Debug.Log("Number of monsters: " + monsters.Length);
    }
}

