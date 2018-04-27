using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedAttack : MonoBehaviour {
    public float chargeFactor = 0f;
    public float dischargeFactor = 0.9f;
    public GameObject sword;
    [SerializeField] private float chargeLevel = 0f;
    public float maxCharge = 10f;
    public Animator animator;
    private bool cooldown = false;

    private IEnumerator DecreaseCharge()
    {
        cooldown = true;
        while (chargeLevel > 0f)
        {
            chargeLevel = chargeLevel * 0.9f;
            if (chargeLevel < 0.01f) chargeLevel = 0f;
            animator.SetFloat("Level", chargeLevel);
            print(chargeLevel);
            yield return new WaitForFixedUpdate();
        }
        cooldown = false;
    }

    private void Attack()
    {
        animator.SetBool("Charging", false);
        animator.SetTrigger("Attack");
        animator.speed = chargeLevel / maxCharge;
        StartCoroutine(DecreaseCharge());
    }

    private void Charge()
    {
        animator.speed = chargeLevel / maxCharge;
        if (chargeLevel < maxCharge)
        {
            if (chargeLevel == 0f)
            {
                animator.SetBool("Charging", true);
            }

            chargeLevel += Time.deltaTime * chargeFactor;
            animator.SetFloat("Level", chargeLevel);
        }
    }

	void Update () {
        if (Input.GetMouseButton(0) && !cooldown)
        {
            Charge();
        }
        else if (Input.GetMouseButtonUp(0) && !cooldown)
        {
            Attack();
        }
	}
}
