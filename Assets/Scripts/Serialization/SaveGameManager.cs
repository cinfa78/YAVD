using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveGameManager : MonoBehaviour {

    public static SaveGameManager instance;
    public SMeshIDs meshIDs;
    public SPlayerStats playerStats;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        //DontDestroyOnLoad(this);
    }

    public void Save(SerializableSaveData data, string name)
    {
        if (!Directory.Exists(Application.dataPath + "/saves"))
            Directory.CreateDirectory(Application.dataPath + "/saves");

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + "/saves/" + name + ".dat");

        formatter.Serialize(file, data);

        file.Close();
    }

    public SerializableSaveData Load(string name)
    {
        if (!File.Exists(Application.dataPath + "/saves/" + name + ".dat"))
            return null;

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Open(Application.dataPath + "/saves/" + name + ".dat", FileMode.Open);

        SerializableSaveData data = (SerializableSaveData)formatter.Deserialize(file);

        file.Close();

        return data;
    }

    public bool LoadGame()
    {
        if (!SaveGameExists())
            return false;
        
        SerializableSaveData data = Load("SaveData");

        SPlayerStats stats = playerStats;

        playerStats.description = data.description;
        playerStats.roomNumber = data.room;
        playerStats.hp = data.hp;

        playerStats.mesh = meshIDs.PlayerMeshes[data.meshID];
        playerStats.sword = meshIDs.SwordMeshes[data.swordID];

        return true;
    }

    public void SaveGame(int roomNumber, float hp, int gold)
    {
        Save(new SerializableSaveData(roomNumber, hp, gold), "SaveData");
    }

    public bool SaveGameExists()
    {
        return File.Exists(Application.dataPath + "/saves/SaveData.dat");
    }

    /* TESTING */

    public int TestNumber = 0;
    public string TestName = "";

    public void SaveTestFile()
    {
        Save(new SerializableSaveData(TestNumber, TestNumber, TestNumber), TestName);
    }

    public void LoadTestFile()
    {
        TestNumber = Load(TestName).room;
    }
}
