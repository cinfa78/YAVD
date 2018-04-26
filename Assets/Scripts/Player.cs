using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public GameObject lookAtObject;
    public GameObject cameraLookAtObject;
    public SVector3Value cameraAimPosition;
    Vector3 lookAtPosition;
    
    public SIntValue maxHp;
    int hp;
    public SEvent playerMove;

    void Awake()
    {
        hp = maxHp.Value;
        lookAtPosition = Vector3.forward;
    }

	void Update () {
        
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit,5000f))
        {
            lookAtPosition = hit.point;
        }


        lookAtPosition = new Vector3(lookAtPosition.x, transform.position.y, lookAtPosition.z);
        


        lookAtObject.transform.position = lookAtPosition;

        cameraAimPosition.Value = transform.position+(lookAtPosition - transform.position) * 0.5f;
        
        cameraLookAtObject.transform.position = Vector3.Lerp(cameraLookAtObject.transform.position, cameraAimPosition.Value, 0.2f);


        Vector3 move = Vector3.zero;
        move.x = Input.GetAxis("Horizontal");
        move.z = Input.GetAxis("Vertical");
        move = Camera.main.transform.forward* Input.GetAxis("Vertical") + Camera.main.transform.right* Input.GetAxis("Horizontal");
        //Camera.main.transform.forward + Camera.main.transform.right;

        transform.LookAt(lookAtPosition);
        transform.position += move;
    }
}
