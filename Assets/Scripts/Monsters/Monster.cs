using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

    public SMonsterBrain brain;
    public SMonsterSharedStats stats;
    public float health;

    void Start()
    {
        health = stats.health;
    }

    void Update () {
        brain.Think(this);
	}

    public float Damage(int damage)
    {
        health -= damage;
        return health;
    }
}
