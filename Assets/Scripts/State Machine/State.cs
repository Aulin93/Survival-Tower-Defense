using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : ScriptableObject
{
    public StateMachine stateMachine {get; set;}

    public virtual void Enter() { }

    public virtual void Run() { }

    public virtual void Exit() { }

    public virtual void RegisterListeners() { }
}
