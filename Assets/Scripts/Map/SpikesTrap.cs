using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesTrap : MonoBehaviour,ITrap {

    public Collider damageArea;
    public GameObject spikes;
    public Animator anim;
	// Use this for initialization
	void Start () {
		
	}
	public void Activate()
    {
        Debug.Log("Spikes Activated");
        anim.SetBool("activate",true);
    }
    
    public void Deactivate()
    {
        Debug.Log("Spikes Lowered");
        anim.SetBool("activate", false);
    }
}
