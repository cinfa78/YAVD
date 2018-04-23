using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEvent : MonoBehaviour {

    public List<SEventListener> listeners = new List<SEventListener>();

    public void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised();
        }
    }

    public void AddListener(SEventListener l)
    {
        listeners.Add(l);
    }
    public void RemoveListener(SEventListener l)
    {
        listeners.Remove(l);
    }
}
