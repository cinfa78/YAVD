using UnityEngine;

public class RoomExit : MonoBehaviour
{
    public SEvent roomExitEvent;
    Collider colliderComponent;

    private void Awake()
    {
        colliderComponent = GetComponent<Collider>();
        colliderComponent.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(colliderComponent);
            roomExitEvent.Raise();
        }
    }

    public void EnableExit(bool e)
    {
        if (colliderComponent)
            colliderComponent.enabled = e;
    }

    public void CloseExit()
    {
        Destroy(colliderComponent);
    }
}
