/*using System.Collections;
using System.Collections.Generic;*/
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    public Collider damageCollider;
    public SFloatValue damage;
    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player") || collider.CompareTag("Monster"))
        {
            //applica danno a chi è entrato nel trigger area
            collider.GetComponent<IDamageable>().Hit(damage.Value);
            print(name + " damages " + collider.name);
        }
    }
}
