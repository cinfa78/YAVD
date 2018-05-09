using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedAttack : MonoBehaviour
{

    public Animator animator;
    public float chargeMultiplier;
    public float maxCharge;
    public float attackRadius;
    public float damageMultiplier;
    public float baseAttackDamage;
    [SerializeField] private float chargeLevel = 0f;

    public MeshRenderer meshRenderer;
    private Material material;
    private float originalSpeed;

    private void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        material = meshRenderer.material;
        originalSpeed = animator.speed;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            animator.SetBool("Charging", true);
            animator.speed = chargeLevel > 1f ? chargeLevel : 1f;

            if (chargeLevel >= maxCharge)
            {
                return;
            }
            material.SetColor("_EmissionColor", (Color.red * chargeLevel / maxCharge));
            chargeLevel += Time.deltaTime * chargeMultiplier;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("Charging", false);
            animator.speed = 1f;

            material.SetColor("_EmissionColor", Color.black);
            chargeLevel = 0f;
            animator.SetTrigger("Attack");

            Damage();
        }
    }

    private void Damage()
    {
        Collider[] enemiesHit;
        enemiesHit = Physics.OverlapSphere(transform.position + Vector3.forward, attackRadius, 1 << 13);
        Vector3 attackFrom = transform.position;
        foreach(Collider enemy in enemiesHit)
        {
            print("Colpito " + enemy + gameObject);
            enemy.GetComponent<Monster>().Damage((damageMultiplier * chargeLevel) + baseAttackDamage, attackFrom);
        }
    }
}
