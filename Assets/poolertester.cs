using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poolertester : MonoBehaviour {

    public GameObjectPool pool;
    public ASAudioEvent spawnSound;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            pool.Spawn("Bolt", transform.position, transform.rotation, Vector3.one);
            spawnSound.Play(GetComponent<AudioSource>());
        }
        if (Input.GetKeyDown(KeyCode.Comma))
            pool.ResetPool();

    }
}
