using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventRich : UnityEvent<EventInfo>
{
}

[CreateAssetMenu(fileName = "Event", menuName = "YAVD/Event")]
public class SEvent : ScriptableObject {

    public List<EventListener> listeners = new List<EventListener>();

    public void Raise(EventInfo eventInfo)
    {
        foreach (EventListener listener in listeners)
        {
            listener.OnEventRaised(eventInfo);
        }
    }
    public void Raise()
    {
        foreach (EventListener listener in listeners)
        {
            listener.OnEventRaised();
        }
    }
    public void AddListener(EventListener l)
    {
        listeners.Add(l);
    }

    public void RemoveListener(EventListener l)
    {
        if(listeners.Contains(l))
            listeners.Remove(l);
    }
}
