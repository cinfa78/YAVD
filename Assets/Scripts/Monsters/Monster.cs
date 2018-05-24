using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Monster : MonoBehaviour, IDamageable
{
    public SMonsterBrain brain;
    public SMonsterSharedStats stats;

    [HideInInspector]
    public float hp;
    [HideInInspector]
    public bool canShoot = true;

    public bool allerted = false;
    public Vector3 target;
    public Vector3 aim;
    public NavMeshAgent agent;
    public SEvent monsterKilled;

    public MonsterState state;

    private void OnEnable()
    {
        aim = Vector3.forward;
    }

    public void GetAllerted(Vector3 target)
    {
        allerted = true;
        this.target = target;
    }

    public void StopAlert()
    {
        allerted = false;
        target = Vector3.up;
    }
    public void Ranged()
    {
        if (canShoot)
            StartCoroutine(RangedCoroutine());
    }
    IEnumerator RangedCoroutine()
    {
        canShoot = false;
        yield return new WaitForSeconds(stats.attackCooldown);
        canShoot = true;
    }

    public void GoTo(Vector3 destination)
    {
        agent.SetDestination(destination);
    }

    void Awake()
    {
        hp = stats.health;
        agent = GetComponent<NavMeshAgent>();
        state = MonsterState.idle;
    }

    void Update()
    {
        brain.Think(this);
    }

    public void Die()
    {
        state = MonsterState.dead;
        monsterKilled.Raise();        
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

    public void EnableAgent(bool enabled)
    {
        agent.enabled = enabled;
    }

    public float Damage(float damage, Vector3 from)
    {
        return brain.Damage(this, damage, from);
    }
}
