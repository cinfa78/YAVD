using UnityEngine;

public interface IMonster{
    void Spawn();
    void Move();
    void AttackMelee();
    void AttackRanged();
    void Hit(float damage);    
    void Die();    
}
