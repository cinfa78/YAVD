using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public Player playerResource;
    public GameObject cameraResource;
    public RoomManager RoomManagerResource;
    public List<SLevel> roomsList;
    SLevel currentRoom;

    Player player;
    RoomManager roomManager;
    public int level = 0;

    void Awake()
    {
        roomManager = Instantiate<RoomManager>(RoomManagerResource);
        player = Instantiate(playerResource) as Player;
        cameraResource = Instantiate<GameObject>(cameraResource);
        player.cameraLookAtObject = cameraResource;
        roomManager.playerStats = player.stats;
        
    }
    void Start()
    {
        currentRoom = roomsList[level];
        roomManager.InitRoom(currentRoom.roomPrefab, currentRoom.monsterConfiguration);
    }
    /*    void Update()
        {

        }*/
}
