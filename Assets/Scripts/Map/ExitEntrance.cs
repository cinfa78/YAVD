using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitEntrance : MonoBehaviour {

    public bool blocked = false;
    public Animator doorAnimator;
    public GameObject playerSpawnPoint;
	
	public void Open()
    {
        if(!blocked)
            doorAnimator.SetTrigger("open");
    }

    public void Close()
    {
        doorAnimator.SetTrigger("close");
    }
    public void Block()
    {
        doorAnimator.SetTrigger("block");
    }

    /*void Update () {
		
	}*/
}
