using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Brain", menuName = "MonsterBrains/Dummy")]
public class SDummyBrain : SMonsterBrain {

    public override void Think(Monster monster)
    {
        if(monster.health <= 0)
        {
            Destroy(monster.gameObject);
        }
    }

    public override float Damage(Monster monster, float damage, Vector3 from)
    {
        monster.GetComponent<Rigidbody>().AddExplosionForce(damage*1000f, from, 10f);
        monster.health -= damage;
        return monster.health;
    }
}
