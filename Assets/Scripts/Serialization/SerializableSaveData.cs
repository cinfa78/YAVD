using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableSaveData {

    public SerializableSaveData(int room, float hp, int gold)
    {
        this.room = room;
        this.hp = hp;
        this.gold = gold;
    }

    public int room;

    public float hp;

    public int gold;

    public string description;

    public int meshID;

    public int swordID;
}
