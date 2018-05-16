using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Brain", menuName = "MonsterBrains/Spider")]
public class SSpiderBrain : SMonsterBrain {

    private void Rotate(Monster monster)
    {
        monster.transform.Rotate(0f, monster.stats.speed, 0f);
    }

    private void AttackPlayer(Monster monster)
    {
        monster.transform.LookAt(monster.aim.transform);
        //Debug.Log("Spider" + monster + " attacks the Player!");
    }

    private void TargetPlayer(Monster monster)
    {
        var angle = Vector3.Angle(monster.transform.forward, Player.instance.transform.forward);
        Debug.Log(angle);

        if (angle < monster.stats.sightAngle)
        {
            //Debug.Log("Spider " + monster + " is facing the Player!");
            monster.GetAllerted(Player.instance.gameObject);
        }
        else
        {
            monster.StopAlert();
        }
    }

    private void UpdateAimPoint(Monster monster)
    {
        if(monster.target != null)
        {
            float aimSpeed = Time.deltaTime * monster.stats.aimSpeed;
            monster.aim.transform.position = Vector3.Lerp(monster.aim.transform.position, monster.target.transform.position, aimSpeed);
        }
    }

    public override void Think(Monster monster)
    {
        // Update the aim point of the spider lerping it
        UpdateAimPoint(monster);

        // Controlla se vede il player, se lo vede lo imposta come target da far seguire alla mira
        TargetPlayer(monster);

        // Se non lo ha visto, idle
        if (!monster.allerted)
        {
            Rotate(monster);
        }

        // Altrimenti attacca
        else
        {
            AttackPlayer(monster);
        }

        // Se la vita è 0, muori
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
