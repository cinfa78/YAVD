using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Brain", menuName = "MonsterBrains/Spider")]
public class SSpiderBrain : SMonsterBrain
{

    public SPlayerStats playerData;

    /*IEnumerator Rotate(Monster monster)
    {
        int rotationDirection = Random.Range(-1,2);
        float t = Time.time;
        float endt = t + Random.Range(1f, 3f);
        while (monster.stats.health > 0)
        {
            if (!monster.allerted)
                yield return null;
            monster.transform.Rotate(0f, monster.stats.speed * rotationDirection, 0f);
            t += Time.deltaTime;
            if (t > endt)
            {
                rotationDirection = Random.Range(-1, 2);
                endt = t + Random.Range(1f, 3f);
            }
            yield return null;
        }
    }*/

    private void Rotate(Monster monster)
    {
        //CAmbiare in Dire a monster di ruotare in una direzione, magari anche se a destra o sinistra        
        monster.transform.Rotate(0f, monster.stats.speed, 0f);
    }

    private void AttackPlayer(Monster monster)
    {
        //Devo dire a monster di sparare lo sputo
        Debug.Log("Spider" + monster + " attacks the Player!");
    }

    float AnglePlayer(Monster monster)
    {
        return Vector3.Angle(monster.transform.forward, playerData.position - monster.transform.position);
    }

    float DistancePlayer(Monster monster)
    {
        return Vector3.Distance(playerData.position, monster.transform.position);
    }

    private void TargetPlayer(Monster monster)
    {
        var distance = DistancePlayer(monster);
        if (distance <= monster.stats.sightDistance)
        {
            var angle = AnglePlayer(monster);
            if (angle < monster.stats.sightAngle)
            {
                
                Ray ray = new Ray(monster.transform.position, playerData.position-monster.transform.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, monster.stats.sightDistance, 1 << 11 | 1 << 12))
                {
                    if (hit.collider.gameObject.layer == 11)
                        monster.GetAllerted(playerData.position);
                    else
                        monster.StopAlert();
                }
                else
                {
                    monster.StopAlert();
                }
            }
            else
            {
                monster.StopAlert();
            }
        }
    }

    private void UpdateAimPoint(Monster monster)
    {
        // Update the aim point of the spider slowling reaching the target
        float aimSpeed = Time.deltaTime * monster.stats.aimSpeed;
        monster.aim = Vector3.Lerp(monster.aim, monster.target, aimSpeed);
        monster.transform.LookAt(monster.aim);
    }

    public override void Think(Monster monster)
    {
        //Checks if player is in sight
        TargetPlayer(monster);
        //if it's not alerted, keeps rotating (randomly?)
        if (!monster.allerted)
        {
            Rotate(monster);
            //aim is in front of the monster
            monster.aim = monster.transform.position + monster.transform.forward * 16f;
        }
        else
        {
            UpdateAimPoint(monster);
            var angle = AnglePlayer(monster);
            if (angle < 1f)
                AttackPlayer(monster);
        }
        
        //Cambiare introducendo gli stati del monster
        if (monster.hp <= 0)
            monster.Die();
    }

    public override float Damage(Monster monster, float damage, Vector3 from)
    {
        monster.GetComponent<Rigidbody>().AddExplosionForce(damage * 1000f, from, 10f);
        monster.hp -= damage;
        return monster.hp;
    }
}
