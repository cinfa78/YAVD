using UnityEngine;
using UnityEngine.Events;

public class EventListener : MonoBehaviour {

    public SEvent Event;
    public UnityEvent Response;


    private void OnEnable()
    {
        Event.AddListener(this);
    }
    private void OnDisable()
    {
        Event.RemoveListener(this);
    }
    public void OnEventRaised()
    {
        Response.Invoke();
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}
