using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Brain", menuName = "MonsterBrains/Spider")]
public class SSpiderBrain : SMonsterBrain {

    private void Rotate(Monster monster)
    {
        monster.transform.Rotate(0f, monster.stats.speed, 0f);
    }

    private void Attack(Monster monster)
    {
        monster.transform.LookAt(monster.target.transform);
        Debug.Log("Spider" + monster + " attacks!");
    }

    public override void Think(Monster monster)
    {
        if (!monster.allerted)
        {
            Rotate(monster);
        }
        else
        {
            Attack(monster);
        }

        if(monster.hp <= 0)
        {
            monster.Die();
        }
    }

    public override float Damage(Monster monster, float damage, Vector3 from)
    {
        monster.GetComponent<Rigidbody>().AddExplosionForce(damage*1000f, from, 10f);
        monster.hp -= damage;
        return monster.hp;
    }
}
