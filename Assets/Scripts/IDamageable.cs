using UnityEngine;

public interface IDamageable {
    void Hit(float damage);
    void Hit(float damage, Vector3 hitter);
}
