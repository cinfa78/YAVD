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
    List<GameObject> spawnPlayer;
    List<GameObject> spawnMonster;
    List<Door> doors;
    List<Player> players;
    List<Monster> monsters;

    List<RoomExit> exits;
    int monstersAlive = 0;
    GameObject room;

    NavMeshSurface surface;

    //public SEvent destroyRoom;

    void Awake()
    {
        room = new GameObject();
        spawnPlayer = new List<GameObject>();
        spawnMonster = new List<GameObject>();
        doors = new List<Door>();
        players = new List<Player>();
        monsters = new List<Monster>();
        exits = new List<RoomExit>();
        surface = GetComponent<NavMeshSurface>();
    }

    /*void Start()
    {
        if (roomPrefabDefault && monstersToSpawnDefault)
            InitRoom(roomPrefabDefault, monstersToSpawnDefault);
    }*/

    public void ClearRoom()
    {
        
        for (int i = room.transform.childCount - 1; i >= 0; i--)
            Destroy(room.transform.GetChild(i));
        Destroy(room);

        for (int i = spawnPlayer.Count - 1; i >= 0; i--)
        {
            Destroy(spawnPlayer[i]);
        }
        spawnPlayer.Clear();
        spawnPlayer.TrimExcess();

        for (int i = spawnMonster.Count - 1; i >= 0; i--)
        {
            Destroy(spawnMonster[i]);
        }
        spawnMonster.Clear();
        spawnMonster.TrimExcess();
        Debug.Log(spawnMonster + " " + spawnMonster.Count + " " + GameObject.FindGameObjectsWithTag("Spawner").Length);


        doors.Clear();
        doors.TrimExcess();

        foreach (Player p in players)
        {
            p.EnableAgent(false);
            p.transform.position -= Vector3.down * 32f;
        }
        monsters.Clear();
        monsters.TrimExcess();
        exits.Clear();
        exits.TrimExcess();

    }

    public void InitRoom(GameObject roomPrefab, SMonsterSpawnConfiguration monstersToSpawn)
    {
        //SaveGameManager.instance.SaveGame(playerStats.roomNumber, playerStats.hp, playerStats.gold);
        int i, j;
        //GameObject room = 
        room = Instantiate(roomPrefab);

        surface.BuildNavMesh();

        players = new List<Player>(GameObject.FindObjectsOfType<Player>());
        foreach (Player p in players)
        {
            p.EnableAgent(false);
        }

        spawnMonster = new List<GameObject>(GameObject.FindGameObjectsWithTag("Spawner"));
        j = 0;
        foreach (SMonsterSpawnConfiguration.Spawner s in monstersToSpawn.monsters)
        {
            for (i = 0; i < s.numberToSpawn; i++)
            {
                //Debug.Log("Instanzio nuovo mostro.");
                GameObject newMonster = Instantiate(s.monsterToSpawn, spawnMonster[j % spawnMonster.Count].transform.position, spawnMonster[j % spawnMonster.Count].transform.rotation);
                newMonster.transform.rotation = spawnMonster[j % spawnMonster.Count].transform.rotation;
                monsters.Add(newMonster.GetComponent<Monster>());
                monstersAlive++;
                //Debug.Log("Mostro " + newMonster.name + " " + monstersAlive + " " + newMonster.transform.position + " " + monsters.Count);
                j++;
            }
        }

        doors = new List<Door>(GameObject.FindObjectsOfType<Door>());

        spawnPlayer = new List<GameObject>(GameObject.FindGameObjectsWithTag("SpawnerPlayer"));

        for (i = 0; i < spawnPlayer.Count; i++)
        {
            RoomExit newSpawnPoint = Instantiate(playerSpawnExitPrefab, spawnPlayer[i].transform.position, spawnPlayer[i].transform.rotation);
            exits.Add(newSpawnPoint);
            newSpawnPoint.EnableExit(false);
            spawnPlayer[i] = newSpawnPoint.gameObject;
        }

        i = 0;
        foreach (Player p in players)
        {
            p.transform.position = spawnPlayer[i % spawnPlayer.Count].transform.position;
            p.transform.rotation = spawnPlayer[i % spawnPlayer.Count].transform.rotation;
            //elimino la  possibilita' di uscire da qua
            spawnPlayer[i % spawnPlayer.Count].GetComponent<RoomExit>().CloseExit();
            //print(p.transform.position + " " + spawnPlayer[i % spawnPlayer.Length].transform.position);
            float minDistance = 80000f;
            int closestDoor = 0;
            for (j = 0; j < doors.Count; j++)
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

        //Debug.Log("Number of monsters: " + monsters.Count + " " + GameObject.FindGameObjectsWithTag("Monster").Length);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            foreach (Monster m in monsters)
            {
                if (m)
                    m.Hit(1000f);
                else
                    monsters.Remove(m);
            }
        }
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
            if (d)
                d.Open();
        }

        foreach (GameObject go in spawnPlayer)
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
            if (d && !d.blocked)
                d.Block();
        }
    }

    public void ExitRoom()
    {
        //destroyRoom.Raise();
        Debug.Log("Player exits room");
        //ClearRoom();
    }
}

