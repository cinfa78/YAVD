using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Brain", menuName = "MonsterBrains/Goblin")]
public class SGoblinBrain : SMonsterBrain
{

    public SPlayerStats playerData;

    private void Rotate(Monster monster)
    {
        //monster.transform.Rotate(0f, monster.stats.speed, 0f);
        monster.Rotate(monster.stats.speed);
    }
    void Wander(Monster monster)
    {
        monster.GoTo(monster.transform.position + new Vector3(Random.value, 0, Random.value));

    }
    private void AttackPlayer(Monster monster)
    {
        //Devo dire a monster di sparare lo sputo

        if (monster.canAttack)
        {
            monster.Ranged();
            GameObjectPool.instance.Spawn("Arrow", monster.transform.position + Vector3.up * 4f, monster.transform.rotation, Vector3.one);
            Debug.Log("Goblin " + monster + " attacks the Player");
        }
    }

    float AnglePlayer(Monster monster)
    {
        return Vector3.Angle(monster.transform.forward, playerData.position - monster.transform.position);
    }

    float DistancePlayer(Monster monster)
    {
        return Vector3.Distance(playerData.position, monster.transform.position);
    }

    void LowerAlert(Monster monster)
    {
        if (monster.state == MonsterState.pursue)
            monster.state = MonsterState.alerted;
        else
            monster.state = MonsterState.idle;
        monster.PauseMovement(2f);
    }

    private void TargetPlayer(Monster monster)
    {
        var distance = DistancePlayer(monster);
        if (distance <= monster.stats.sightDistance)
        {
            var angle = AnglePlayer(monster);
            if (angle < monster.stats.sightAngle)
            {
                Ray ray = new Ray(monster.transform.position, playerData.position - monster.transform.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, monster.stats.sightDistance, 1 << 11))//| 1 << 12
                {
                    if (hit.collider.gameObject.layer == 11)
                    {
                        monster.state = MonsterState.pursue;
                    }
                    else
                    {
                        LowerAlert(monster);
                    }

                }
                else
                {
                    LowerAlert(monster);
                }
            }
            else
            {
                LowerAlert(monster);
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


        switch (monster.state)
        {
            case MonsterState.idle:
                Rotate(monster);
                monster.aim = monster.transform.position + monster.transform.forward * 16f;
                TargetPlayer(monster);
                break;
            case MonsterState.pursue:
                monster.GoTo(monster.target);
                if (DistancePlayer(monster) < monster.stats.attackDistance)
                {
                    monster.state = MonsterState.attack;
                }
                break;
            case MonsterState.attack:
                TargetPlayer(monster);
                if (AnglePlayer(monster) < 1f)
                    AttackPlayer(monster);                
                break;
            default:
                TargetPlayer(monster);
                break;
        }


        //if it's not alerted, keeps rotating (randomly?)
        if (!monster.allerted)
        {

            //aim is in front of the monster

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
