using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CustomEvent
{
    public string eventDescription;

    public CustomEvent(string description)
    {
        eventDescription = description;
    }
}
