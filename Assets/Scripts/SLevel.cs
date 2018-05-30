using UnityEngine;

[CreateAssetMenu(menuName ="YAVD/Level", fileName ="New Room")]
public class SLevel : ScriptableObject {
    public GameObject roomPrefab;
    public SMonsterSpawnConfiguration monsterConfiguration;	
}
