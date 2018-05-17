using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="YAVD/Map Layer")]
public class SLayerMap : ScriptableObject {
    public string tag;
    public Vector2 size;
    public List<GameObject> map;
    public List<GameObject> items;
    public List<GameObject> spawnPoints;
    public List<GameObject> exits;
    public List<GameObject> entrances;
}
