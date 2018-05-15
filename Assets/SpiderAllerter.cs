using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAllerter : MonoBehaviour {
    Monster behaviour;
    private float sight;

	void Start () {
        behaviour = GetComponent<Monster>();
        sight = behaviour.stats.sight;
	}
	
	void Update () {
        Ray ray = new Ray(transform.position, Vector3.forward);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, sight))
        {
            if(hit.collider.tag == "Player")
            {
                behaviour.GetAllerted(hit.collider.gameObject);
            }
        }
	}
}
