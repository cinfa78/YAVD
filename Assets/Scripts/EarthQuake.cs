using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthQuake : MonoBehaviour {

    GameObject[] tiles;
    // Use this for initialization
    void Start()
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile");
    }

	void Update () {
        if (Input.GetKey(KeyCode.T))
        {
            foreach (GameObject t in tiles)
            {
                t.transform.localPosition += Vector3.one * Random.Range(-0.1f, 0.1f);
            }
        }
        else if (Input.GetKeyUp(KeyCode.T))
        {
            foreach (GameObject t in tiles)
            {
                //t.transform.localPosition = Vector3.zero;
            }
        }
        
	}
}
