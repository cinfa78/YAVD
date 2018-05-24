using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnMap : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        //print(collision.gameObject.name+ " " + collision.gameObject.layer);
        if (collision.gameObject.layer == 12)
            gameObject.SetActive(false);
    }
}
