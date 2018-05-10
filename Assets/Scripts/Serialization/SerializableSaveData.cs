using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableSaveData {

    public SerializableSaveData(int room)
    {
        this.room = room;
    }

    public int room;
}
