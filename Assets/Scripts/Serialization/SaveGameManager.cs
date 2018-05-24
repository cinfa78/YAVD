using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveGameManager : MonoBehaviour {

    public static SaveGameManager instance;

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

    public void SaveRoom(int roomNumber)
    {
        Save(new SerializableSaveData(roomNumber), "SaveData");
    }

    /* TESTING */

    public int TestNumber = 0;
    public string TestName = "";

    public void SaveTestFile()
    {
        Save(new SerializableSaveData(TestNumber), TestName);
    }

    public void LoadTestFile()
    {
        TestNumber = Load(TestName).room;
    }
}
