using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//La classe Tile e' associata ai prefab delle tile in modo che abbiano dei comportamenti particolari al momento del'Awake
public class TileRandomizer : MonoBehaviour
{

    public List<float> possibleRandomAngles;
    public List<GameObject> meshes;
    GameObject mesh;
    // Use this for initialization
    void Awake()
    {
        foreach(GameObject m in meshes)
        {
            m.SetActive(false);
        }
        mesh = meshes[Random.Range(0, meshes.Count)];
        mesh.SetActive(true);
        if (possibleRandomAngles.Count > 0)
        {
            mesh.transform.Rotate(Vector3.up, possibleRandomAngles[Random.Range(0, possibleRandomAngles.Count)]);
        }
    }

}
