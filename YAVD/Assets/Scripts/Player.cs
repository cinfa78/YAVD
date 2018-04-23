using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public SIntValue maxHp;
    int hp;
    public SEvent playerMove;

    void Awake()
    {
        hp = maxHp.Value;
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
