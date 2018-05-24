﻿using System.Collections;
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
    public bool canAttack = true;
    [HideInInspector]
    public bool canMove = true;

    public bool allerted = false;
    public Vector3 target;
    public Vector3 aim;
    public NavMeshAgent agent;
    public SEvent monsterKilled;

    public MonsterState state;

    private void OnEnable()
    {
        aim = Vector3.forward;
        agent.speed = stats.speed;
    }

    public void GetAllerted(Vector3 target)
    {
        allerted = true;
        SetTarget(target);
        state = MonsterState.alerted;
    }

    public void SetTarget(Vector3 target)
    {
        this.target = target;
    }

    public void StopAlert()
    {
        allerted = false;
        state = MonsterState.idle;
        target = Vector3.up;
    }
    public void Melee()
    {
        if (canAttack)
            StartCoroutine(MeleeCoroutine());
    }
    IEnumerator MeleeCoroutine()
    {
        canAttack = false;
        yield return new WaitForSeconds(stats.attackCooldown);
        canAttack = true;
    }

    public void Ranged()
    {
        if (canAttack)
            StartCoroutine(RangedCoroutine());
    }

    IEnumerator RangedCoroutine()
    {
        canAttack = false;
        yield return new WaitForSeconds(stats.attackCooldown);
        canAttack = true;
    }

    public void PauseMovement(float seconds)
    {
        if(canMove)
            StartCoroutine(PauseMovementCoroutine(seconds));
    }

    IEnumerator PauseMovementCoroutine(float seconds)
    {
        canMove = false;
        agent.velocity *= 0.5f;
        yield return new WaitForSeconds(seconds);
        canMove = true;
    }

    public void GoTo(Vector3 destination)
    {
        if(canMove)
            agent.SetDestination(destination);
    }

    public void Rotate(float yAngle)
    {
        if(canMove)
            transform.Rotate(Vector3.up * yAngle);
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
