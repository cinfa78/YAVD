using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnMap : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 12)
            gameObject.SetActive(false);
    }
}
