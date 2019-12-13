using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public Player playerResource;
	public GameObject cameraResource;
	public RoomManager RoomManagerResource;
	public List<SLevel> roomsList;
	SLevel currentRoom;

	Player player;
	RoomManager roomManager;
	public int level = 0;

	private void Awake() {
		roomManager = Instantiate<RoomManager>(RoomManagerResource);
		player = Instantiate(playerResource) as Player;
		cameraResource = Instantiate<GameObject>(cameraResource);
		player.cameraLookAtObject = cameraResource;
		roomManager.playerStats = player.stats;
		Cursor.visible = true;
	}

	private void Start() {
		roomManager.ClearRoom();
		InitRoom();
	}

	public void LoadNextLevel() {
		level++;
		if (level >= roomsList.Count) {
			Debug.Log("Fine livelli");
		}
		else {
			roomManager.ClearRoom();
			InitRoom();
		}
	}

	public void InitRoom() {
		currentRoom = roomsList[level];
		roomManager.InitRoom(currentRoom.roomPrefab, currentRoom.monsterConfiguration);
	}
}