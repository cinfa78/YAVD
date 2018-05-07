using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedAttack : MonoBehaviour
{

    public Animator animator;
    public float chargeMultiplier;
    public float maxCharge;
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

            material.SetColor("_EmissionColor", Color.black);
            chargeLevel = 0f;
            animator.SetTrigger("Attack");
        }
    }
}
