using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapActivator : MonoBehaviour
{
    public Collider activationCollider;
    public ITrap trap;
    void Awake()
    {
        trap = GetComponent<ITrap>();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trap Activated");
        if (other.CompareTag("Player") || other.CompareTag("Monster"))
        {
            trap.Activate();
        }
    }
    void OnTriggerExit(Collider other)
    {
        Debug.Log("Trap Deactivated");
        if (other.CompareTag("Player") || other.CompareTag("Monster"))
        {
            trap.Deactivate();
        }
    }
}
