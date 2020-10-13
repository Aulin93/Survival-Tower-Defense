using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameControllerState/PreparationState")]
public class GameControllerPreparationState : GameControllerBaseState
{
    [SerializeField] private float preparationTime = 60;
    private float counter;

    public override void Enter()
    {
        counter = 0;
        EventCoordinator.FireEvent(new PreparationStageStartEvent(preparationTime));
    }

    public override void Run()
    {
        counter += Time.deltaTime;
        if(counter >= preparationTime) { stateMachine.TransitionTo<GameControllerSpawnWaveState>(); }
    }
}
