using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshSurface))]
public class RoomManager : MonoBehaviour
{
    //temporaneo
    public GameObject roomPrefabDefault;
    public SMonsterSpawnConfiguration monstersToSpawnDefault;
    public RoomExit playerSpawnExitPrefab;
    public SPlayerStats playerStats;
    GameObject[] spawnPlayer;
    GameObject[] spawnMonster;
    Door[] doors;
    RoomExit[] exits;
    Player[] players;
    List<Monster> monsters;
    int monstersAlive = 0;

    NavMeshSurface surface;

    void Awake()
    {
        monsters = new List<Monster>();
    }

    void Start()
    {
        if (roomPrefabDefault && monstersToSpawnDefault)
            InitRoom(roomPrefabDefault, monstersToSpawnDefault);
        //SPlayerStats stats = Player.instance.stats;
        
    }

    public void ClearRoom()
    {
        foreach (Monster m in monsters)
        {
            Destroy(m);
        }
        monsters.Clear();
        for (int i = 0; i < spawnPlayer.Length; i++)
        {
            /*RoomExit newSpawnPoint = Instantiate(playerSpawnExitPrefab, spawnPlayer[i].transform.position, spawnPlayer[i].transform.rotation);
            newSpawnPoint.EnableExit(false);*/
            Destroy(spawnPlayer[i]);
        }
        spawnPlayer = null;
    }

    public void InitRoom(GameObject roomPrefab, SMonsterSpawnConfiguration monstersToSpawn)
    {
        //SaveGameManager.instance.SaveGame(playerStats.roomNumber, playerStats.hp, playerStats.gold);
        int i, j;
        //GameObject room = 
        Instantiate(roomPrefab);

        surface = GetComponent<NavMeshSurface>();
        surface.BuildNavMesh();

        players = GameObject.FindObjectsOfType<Player>();
        foreach (Player p in players)
        {
            p.EnableAgent(false);
        }

        spawnMonster = GameObject.FindGameObjectsWithTag("Spawner");
        j = 0;
        foreach (SMonsterSpawnConfiguration.Spawner s in monstersToSpawn.monsters)
        {
            for (i = 0; i < s.numberToSpawn; i++)
            {
                GameObject newMonster = Instantiate(s.monsterToSpawn, spawnMonster[j % spawnMonster.Length].transform);
                newMonster.transform.rotation = spawnMonster[j % spawnMonster.Length].transform.rotation;
                monsters.Add(newMonster.GetComponent<Monster>());
                monstersAlive++;
                j++;
            }
        }

        doors = GameObject.FindObjectsOfType<Door>();

        spawnPlayer = GameObject.FindGameObjectsWithTag("SpawnerPlayer");

        for (i = 0; i < spawnPlayer.Length; i++)
        {
            RoomExit newSpawnPoint = Instantiate(playerSpawnExitPrefab, spawnPlayer[i].transform.position, spawnPlayer[i].transform.rotation);
            newSpawnPoint.EnableExit(false);
            spawnPlayer[i] = newSpawnPoint.gameObject;
        }

        i = 0;
        foreach (Player p in players)
        {
            p.transform.position = spawnPlayer[i % spawnPlayer.Length].transform.position;            
            p.transform.rotation = spawnPlayer[i % spawnPlayer.Length].transform.rotation;
            //elimino la  possibilita' di uscire da qua
            spawnPlayer[i % spawnPlayer.Length].GetComponent<RoomExit>().CloseExit();
            //print(p.transform.position + " " + spawnPlayer[i % spawnPlayer.Length].transform.position);
            float minDistance = 80000f;
            int closestDoor = 0;
            for (j = 0; j < doors.Length; j++)
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

        Debug.Log("Number of monsters: " + monsters.Count);
    }
    public void MonsterKilled()
    {
        monstersAlive--;
        if (monstersAlive == 0)
        {
            OpenDoors();
        }
    }
    public void OpenDoors()
    {
        foreach (Door d in doors)
        {
            d.Open();
        }
        foreach(GameObject go in spawnPlayer)
        {
            RoomExit temp = go.GetComponent<RoomExit>();
            if (temp)
                temp.EnableExit(true);
        }
    }
    public void BlockDoors()
    {
        foreach (Door d in doors)
        {
            if (!d.blocked)
                d.Block();
        }
    }

    public void ExitRoom()
    {
        Debug.Log("Player exits room");
        ClearRoom();
    }
}

