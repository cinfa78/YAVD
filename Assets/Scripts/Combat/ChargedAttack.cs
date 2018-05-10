using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedAttack : MonoBehaviour
{
    public SEvent slashEvent;
    public SWeaponStats stats;
    private EventInfo slashInfo;
    public Animator animator;
    public float attackRadius;
    [SerializeField] private float chargeLevel = 0f;

    public MeshRenderer meshRenderer;
    private Material material;
    private float originalSpeed;

    private void Awake()
    {

        slashInfo = new EventInfo();
        slashInfo.AddFloat("damage", stats.damage);
    }

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
            animator.speed = chargeLevel/stats.fullChargeTime;

            if (chargeLevel >= stats.fullChargeTime)
            {
                chargeLevel = stats.fullChargeTime;
                return;
            }
            material.SetColor("_EmissionColor", (Color.red * chargeLevel / stats.fullChargeTime));
            chargeLevel += Time.deltaTime;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("Charging", false);
            animator.speed = 1f;
            slashInfo.SetFloat("damage", stats.damage + stats.chargedDamage * chargeLevel / stats.fullChargeTime);            
            animator.SetTrigger("Attack");            
            slashEvent.Raise(slashInfo);
            material.SetColor("_EmissionColor", Color.black);
            chargeLevel = 0f;
            //Damage();
        }
    }

    //da cambiare!
    /*private void Damage()
    {
        Collider[] enemiesHit;
        enemiesHit = Physics.OverlapSphere(transform.position + Vector3.forward, attackRadius, 1 << 13);
        Vector3 attackFrom = transform.position;
        foreach(Collider enemy in enemiesHit)
        {
            print("Colpito " + enemy + gameObject);
            enemy.GetComponent<Monster>().Damage((stats.chargedDamage * chargeLevel/ stats.fullChargeTime) + stats.damage, attackFrom);
        }
    }*/
}
