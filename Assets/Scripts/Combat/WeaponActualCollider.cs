using UnityEngine;

public class WeaponActualCollider : MonoBehaviour {
    public ChargedAttack masterWeaponGameObject;
    private void OnTriggerEnter(Collider other)
    {
        masterWeaponGameObject.DoDamage(other);
    }
}
