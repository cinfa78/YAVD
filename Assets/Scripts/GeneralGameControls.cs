using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralGameControls : MonoBehaviour {

    public SEvent pauseButtonEvent;
    public SEvent startButtonEvent;
    void Update () {
        if (Input.GetButtonDown("Start"))
        {
            Debug.Log("Start Game");
            startButtonEvent.Raise();
        }
        if (Input.GetButtonDown("Pause"))
        {
            pauseButtonEvent.Raise();
            Debug.Log("Pause Game");
        }
        if (Input.GetButtonDown("Quit"))
        {
            Application.Quit();
        }
	}
}
