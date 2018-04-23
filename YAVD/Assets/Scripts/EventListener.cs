using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(fileName ="NewEventListener",menuName ="YAVD/Event Listener")]
public class SEventListener : ScriptableObject {

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
}
