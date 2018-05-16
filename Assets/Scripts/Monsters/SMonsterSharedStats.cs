using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterStats", menuName = "MonsterStats")]
public class SMonsterSharedStats : ScriptableObject {
    public float damage;
    public float attackCooldown;
    public float health;
    public float speed;
    public float sight;
    public float sightAngle;
    public float aimSpeed;
}
