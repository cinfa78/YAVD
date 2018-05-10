using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(ParticleSystem))]
public class ParticleFx : MonoBehaviour,IPooledObject {

    ParticleSystem ps;
    public void OnSpawn () {
        ps.Play();
    }
	
	public void OnDespawn () {
        ps.Clear();
	}
}
