using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Player : MonoBehaviour,IDamageable {
    public SPlayerStats statsDefault;
    public SPlayerStats stats;
    public GameObject lookAtObject;
    public GameObject movingDirectionObject;
    public GameObject cameraLookAtObject;
    public SVector3Value cameraAimPosition;
    Quaternion directionToFace;
    Vector3 lookAtPosition;
    public ASAudioEvent stepSound;
    AudioSource audioSource;
    
    public SEvent playerMove;
    public SEvent playerDeath;
    public SEvent playerStatsChangeEvent;
    float speed;
    Vector3 previousPosition;
    NavMeshAgent agent;

    #region"Public static reference to this"
    public static Player instance = null;

    private void OnEnable()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void OnDisable()
    {
        instance = null;
    }
    #endregion

    private void Reset()
    {
        if(stats == null)
        {
            stats = ScriptableObject.CreateInstance("SPlayerStats") as SPlayerStats;
        }
        stats.hp = statsDefault.hp;
        stats.gold = statsDefault.gold;
        stats.position = statsDefault.position;
        stats.facingDirection = statsDefault.facingDirection;


    }

    void Awake()
    {        
        lookAtPosition = Vector3.forward;
        speed = 0;
        previousPosition = transform.position;
        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        GameObject meshContainer = transform.Find("MeshContainer").gameObject;
        GameObject mesh = GameObject.Instantiate(stats.mesh,meshContainer.transform) as GameObject;
        GameObject swordContainer = transform.Find("SwordAttachment").gameObject;
        mesh = GameObject.Instantiate(stats.sword, swordContainer.transform) as GameObject;        
        if (stats == null) Reset();
        if (stats.hp <= 0) Reset();
    }
    void Start()
    {
        
    }

    public void Hit(float damageReceived)
    {
        stats.hp -= damageReceived;
        playerStatsChangeEvent.Raise();
        if (stats.hp <= 0)
        {
            stats.hp = 0f;
            playerDeath.Raise();
        }
    }

	void Update () {

        /*if (Input.GetKeyDown(KeyCode.N))
        {
            NavMeshHit closestHit;

            if (NavMesh.SamplePosition(gameObject.transform.position, out closestHit, 500f, NavMesh.AllAreas)) { 
                gameObject.transform.position = closestHit.position;
                //agent = gameObject.AddComponent<NavMeshAgent>();
            }
            else
                Debug.LogError("Could not find position on NavMesh!");
        }*/

        previousPosition = transform.position;
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit,5000f,1<<8))
        {
            lookAtPosition = hit.point;
        }

        lookAtPosition = new Vector3(lookAtPosition.x, transform.position.y, lookAtPosition.z);

        if(lookAtObject)
            lookAtObject.transform.position = lookAtPosition;

        cameraAimPosition.Value = transform.position+(lookAtPosition - transform.position) * 0.5f;
        
        cameraLookAtObject.transform.position = Vector3.Lerp(cameraLookAtObject.transform.position, cameraAimPosition.Value, 0.2f);

        directionToFace = Quaternion.LookRotation(lookAtPosition - transform.position);
        //Ruota verso il mouse
        transform.rotation = Quaternion.Lerp(transform.rotation, directionToFace, 0.1f);

        Vector3 move = Vector3.zero;
        move.x = Input.GetAxis("Horizontal");
        move.y = Input.GetAxis("Vertical");
        //move = Camera.main.transform.forward * Input.GetAxis("Vertical") + Camera.main.transform.right * Input.GetAxis("Horizontal");

        //debug
        if (move.magnitude > Mathf.Epsilon)
        {
            Vector3 destinationPoint = Camera.main.WorldToScreenPoint(transform.position) + move.normalized *16f;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(destinationPoint), out hit, 5000f, 1 << 8))
            {
                agent.SetDestination(hit.point);
                //print(transform.position+" "+hit.point);
            }
        }
        else { 
            agent.SetDestination(transform.position);
            agent.velocity *= 0.5f; ;
            agent.ResetPath();
        }
        movingDirectionObject.transform.position = agent.destination;
        //transform.position += move;

        stats.position = transform.position;
    }
    public void EnableAgent(bool enabled)
    {
        agent.enabled = enabled;
    }
    private void LateUpdate()
    {
        speed = Vector3.Magnitude(transform.position - previousPosition) / Time.deltaTime;
        if (speed > 0) playerMove.Raise(); 
    }

}
