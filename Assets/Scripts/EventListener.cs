using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventListener : MonoBehaviour {

    public SEvent Event;
    public EventRich ResponseInfo;
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
    public void OnEventRaised(EventInfo eventInfo)
    {
        ResponseInfo.Invoke(eventInfo);
    }
}
