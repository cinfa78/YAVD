using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour,IDamageable {

    public SMonsterBrain brain;
    public SMonsterSharedStats stats;
    
    [HideInInspector] public float hp;

    void Awake()
    {
        hp = stats.health;
    }

    void Update () {
        brain.Think(this);
	}

    public void Die()
    {
        Destroy(gameObject);
    }

    public void Hit(float damageReceived)
    {
        print(name + " has been Hit");
        GameObjectPool.instance.Spawn("Blood", transform.position + Vector3.up * 8f, transform.rotation, Vector3.one);
        hp -= damageReceived;
        if (hp <= 0)
        {
            hp = 0f;
        }
    }
    public float Damage(float damage, Vector3 from)
    {
        return brain.Damage(this, damage, from);
    }
}
