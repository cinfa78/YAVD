using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour {

    public SVector3Value cameraRotation;
	void Update () {
        
        if (Input.GetKey(KeyCode.Q))
        {
            cameraRotation.SetY(cameraRotation.Value.y + 1f);
        }
        if (Input.GetKey(KeyCode.E))
        {
            cameraRotation.SetY(cameraRotation.Value.y - 1f);
        }
        transform.rotation = Quaternion.Euler(cameraRotation.Value);
    }
}
