using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {
    
    
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Mouse ScrollWheel")!=0)
        {
            Camera.main.orthographicSize -= Input.GetAxis("Mouse ScrollWheel")*10;
        }
	}
}
