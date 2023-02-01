using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/EventRouter")]
public class EventRouter : ScriptableObject
{
    // allow observers to subscribe to event ( onEvent += )
    public UnityAction onEvent;
    public void Notify()
    {
        // notify all observers of event
        onEvent?.Invoke();
    }
}