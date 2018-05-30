using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Brain", menuName = "MonsterBrains/Goblin")]
public class SGoblinBrain : SMonsterBrain
{
    public SPlayerStats playerData;

    private void Rotate(Monster monster, int direction = 1)
    {
        //monster.transform.Rotate(0f, monster.stats.speed, 0f);
        monster.Rotate(direction);
    }
    void Wander(Monster monster)
    {
        monster.GoTo(monster.transform.position + new Vector3(Random.value, 0, Random.value));

    }
    private void AttackPlayer(Monster monster)
    {
        if (monster.canAttack)
        {
            Ray ray = new Ray(monster.transform.position, monster.aim - monster.transform.position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, monster.stats.sightDistance, 1 << 11 | 1 << 12))
            {
                if (hit.collider.gameObject.layer == 11)
                {
                    monster.Ranged();
                    GameObject arrow = GameObjectPool.instance.Spawn("Arrow", monster.transform.position + Vector3.up * 4f, monster.transform.rotation, Vector3.one);
                    arrow.GetComponent<Projectile>().owner = monster.gameObject;
                    Debug.Log("Goblin " + monster + " attacks the Player");
                }
            }
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
                if (Physics.Raycast(ray, out hit, monster.stats.sightDistance, 1 << 11 | 1 << 12))//| 1 << 12
                {
                    if (hit.collider.gameObject.layer == 11)
                    {
                        monster.state = MonsterState.pursue;
                        monster.target = playerData.position;
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
        monster.aim = Vector3.Lerp(monster.aim, monster.target, Time.deltaTime * monster.stats.aimSpeed);
        monster.transform.LookAt(monster.aim);
    }

    public override void Think(Monster monster)
    {
        //Checks if player is in sight
        Vector3 rotation = monster.transform.rotation.eulerAngles;
        /*monster.transform.LookAt(monster.target);
        float rotateDirection = ((monster.transform.rotation.eulerAngles.y - rotation.y + 360f) % 360f) > 180.0f ? -1 : 1;
        monster.transform.rotation = Quaternion.Euler(rotation);*/
        switch (monster.state)
        {
            case MonsterState.idle:
                monster.aim = monster.transform.position + monster.transform.forward * 16f;
                if (Random.value > 0.7f)
                {
                    if (Random.value > 0.2f)
                        Rotate(monster);
                    else
                        monster.GoTo(monster.aim);
                }
                else
                {
                    //Debug.Log("stay...");
                }
                TargetPlayer(monster);
                break;
            case MonsterState.alerted:
                monster.aim = monster.transform.position + monster.transform.forward * 16f;
                if (Random.value > 0.2f)
                {
                    Rotate(monster);
                }
                else
                {
                    monster.GoTo(monster.aim);
                }
                TargetPlayer(monster);
                break;
            case MonsterState.pursue:
                TargetPlayer(monster);
                monster.GoTo(monster.target);
                if (DistancePlayer(monster) < monster.stats.attackDistance)
                {
                    //mi fermo
                    monster.aim = playerData.position;
                    monster.GoTo(monster.transform.position);
                    monster.agent.velocity *= 0.5f;
                    //voglio attaccare
                    monster.state = MonsterState.attack;
                }
                else if (DistancePlayer(monster) > monster.stats.sightAngle)
                {
                    LowerAlert(monster);
                }
                break;
            case MonsterState.attack:
                //TargetPlayer(monster);
                //monster.GoTo(monster.transform.position);
                //miro
                monster.target = playerData.position;
                UpdateAimPoint(monster);
                if (AnglePlayer(monster) < 1f)
                {
                    AttackPlayer(monster);
                    monster.state = MonsterState.pursue;
                }
                break;
            case MonsterState.dead: break;
            case MonsterState.hurt: break;
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
