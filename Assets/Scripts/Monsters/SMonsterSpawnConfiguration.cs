using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="YAVD/Monster Spawn Configuration")]
public class SMonsterSpawnConfiguration : ScriptableObject
{
    [System.Serializable]
    public class Spawner
    {        
        public GameObject monsterToSpawn;
        public int numberToSpawn;
    }
    public List<Spawner> monsters;
}

