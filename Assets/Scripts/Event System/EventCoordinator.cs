using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventCoordinator
{

    public delegate void EventListener(CustomEvent eventInfo);

    private static Dictionary<System.Type, List<EventListener>> eventListeners = new Dictionary<System.Type, List<EventListener>>();

    public static void RegisterListener<EventType>(System.Action<EventType> listener) where EventType : CustomEvent
    {
        System.Type eventType = typeof(EventType);
        if (!eventListeners.ContainsKey(eventType))
        {
            eventListeners.Add(eventType, new List<EventListener>());
        }
        EventListener wrapper = (wrappedEventListener) => { listener((EventType)wrappedEventListener); };
        eventListeners[eventType].Add(wrapper);
    }

    public static void UnregisterListener<EventType>(System.Action<EventType> listener) where EventType : CustomEvent
    {
        System.Type eventType = typeof(EventType);
        EventListener wrapper = (wrappedEventListener) => { listener((EventType)wrappedEventListener); };
        eventListeners[eventType].Remove(wrapper);
    }

    public static void FireEvent(CustomEvent firedEvent)
    {
        System.Type eventType = firedEvent.GetType();
        if (eventListeners.ContainsKey(eventType))
        {
            if (eventListeners[eventType].Count > 0)
            {
                List<EventListener> listeners = eventListeners[eventType];
                for(int i = 0; i < listeners.Count; i++)
                {
                    listeners[i](firedEvent);
                }
            }
        }
    }
}
