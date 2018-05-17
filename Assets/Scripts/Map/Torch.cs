using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour,IInteractable {

    Animator anim;
    public bool lit;
	// Use this for initialization
	void Awake () {
        anim = GetComponent<Animator>();
	}
	void Start()
    {
        anim.speed = Random.Range(0.5f, 1f);
        anim.SetBool("lit", lit);
    }
	
	public void Interact () {
        lit = !lit;
        anim.SetBool("lit", lit);
	}
}
