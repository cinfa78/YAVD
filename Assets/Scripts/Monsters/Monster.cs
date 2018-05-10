using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

    public SMonsterBrain brain;
    public SMonsterSharedStats stats;
    [SerializeField] public float health;

    void Start()
    {
        health = stats.health;
    }

    void Update () {
        brain.Think(this);
	}
    public void CheckDamage(EventInfo slashInfo)
    {
        if (true)
            Damage(slashInfo.GetFloat("damage"),Vector3.zero);
    }

    public float Damage(float damage, Vector3 from)
    {
        return brain.Damage(this, damage, from);
    }
}
