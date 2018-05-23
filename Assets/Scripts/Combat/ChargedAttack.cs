using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedAttack : MonoBehaviour
{
    
    public SWeaponStats stats;    
    public Animator animator;
    public float attackRadius;
    [SerializeField] private float chargeLevel = 0f;

    public MeshRenderer meshRenderer;
    private Material material;
    private float originalSpeed;
    float damage;

    /*private void Awake()
    {
        
    }*/

    private void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        material = meshRenderer.material;
        originalSpeed = animator.speed;
    }

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            animator.SetBool("Charging", true);
            animator.speed = chargeLevel/stats.fullChargeTime;

            if (chargeLevel >= stats.fullChargeTime)
            {
                chargeLevel = stats.fullChargeTime;
                return;
            }
            material.SetColor("_EmissionColor", (Color.red * chargeLevel / stats.fullChargeTime));
            chargeLevel += Time.deltaTime;
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            animator.SetBool("Charging", false);
            animator.speed = 1f;
            damage = stats.damage + stats.chargedDamage * chargeLevel / stats.fullChargeTime;
            animator.SetTrigger("Attack");

            material.SetColor("_EmissionColor", Color.black);
            chargeLevel = 0f;
        }
    }

    public void DoDamage(Collider other)
    {        
        if (other.gameObject.GetComponent<IDamageable>() != null && other.tag!="Player")
        {
            other.gameObject.GetComponent<IDamageable>().Hit(damage);
        }
    }
}
