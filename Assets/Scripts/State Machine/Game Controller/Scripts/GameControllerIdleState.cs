using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameControllerState/IdleState")]
public class GameControllerIdleState : GameControllerBaseState
{
    public override void RegisterListeners()
    {
        EventCoordinator.RegisterListener<BaseConstructedEvent>(OnBaseConstructed);
    }

    public void OnBaseConstructed(BaseConstructedEvent baseConstructed)
    {
        stateMachine.TransitionTo<GameControllerPreparationState>();
    }
}
