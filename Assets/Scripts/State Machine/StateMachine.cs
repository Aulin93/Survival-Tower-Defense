using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private Dictionary<System.Type, State> stateDictionary = new Dictionary<System.Type, State>();
    public Object owner { get; }
    private State currentState;
    private Stack<State> queuedStates = new Stack<State>();

    public StateMachine(Object machineOwner, State[] states)
    {
        owner = machineOwner;
        for(int i = 0; i < states.Length; i++)
        {
            State instance = Object.Instantiate(states[i]);
            instance.stateMachine = this;
            instance.RegisterListeners();
            stateDictionary.Add(instance.GetType(), instance);
            if(currentState == null)
            {
                currentState = instance;
            }
        }
        currentState?.Enter();
    }

    public void Run()
    {
        currentState.Run();
        if(queuedStates.Count > 0)
        {
            ChangeState();
            queuedStates.Clear();
        }
    }

    public void TransitionTo<T>() where T: State
    {
        queuedStates.Push(stateDictionary[typeof(T)]);
    }

    public void ChangeState()
    {
        currentState?.Exit();
        currentState = queuedStates.Pop();
        currentState.Enter();
    }
}
