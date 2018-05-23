using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour {

    public SVector3Value cameraRotation;
	void Update () {

        if (Input.GetAxis("CameraRotate") != 0)
        {
            /*    cameraRotation.SetY(cameraRotation.Value.y + 1f);
            }
            if (Input.GetKey(KeyCode.E))
            {
                cameraRotation.SetY(cameraRotation.Value.y - 1f);
            }*/

            cameraRotation.SetY(cameraRotation.Value.y + Input.GetAxis("CameraRotate"));
        }
        transform.rotation = Quaternion.Euler(cameraRotation.Value);
    }
}
