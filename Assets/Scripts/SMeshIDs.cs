using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;


[CreateAssetMenu(menuName = "YAVD/Mesh IDs Holder")]
public class SMeshIDs : ScriptableObject {
    [Header("Gli id vanno in ordine di inserimento")]
    public GameObject[] PlayerMeshes;
    public GameObject[] SwordMeshes;
}
