using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandForceChangedEvent : CustomEvent
{
    public int commandForceChange;
    public int currentCommandForce;

    public CommandForceChangedEvent(int commandChange, int newCurrentCommand) : base("This is a command force has changed event.")
    {
        commandForceChange = commandChange;
        currentCommandForce = newCurrentCommand;
    }
}
