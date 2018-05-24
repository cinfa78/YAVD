using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterStats", menuName = "MonsterStats")]
public class SMonsterSharedStats : ScriptableObject {
    public float damage;    
    public float attackCooldown;
    public float health;
    public float speed;
    public float sightDistance;
    public float attackDistance;
    public float sightAngle;
    public float aimAngle;
    public float aimSpeed;
}
