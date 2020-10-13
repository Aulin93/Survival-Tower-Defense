using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLostEvent : CustomEvent
{
    public PlayerLostEvent() : base("This is a player lost the game event.") { }
}
