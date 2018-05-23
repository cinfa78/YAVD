using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralGameControls : MonoBehaviour {

	
	void Update () {
        if (Input.GetButtonDown("Pause"))
        {
            Debug.Log("Pause Game");
        }
        if (Input.GetButtonDown("Quit"))
        {
            Application.Quit();
        }
	}
}
