using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MissileState/InitialBurnState")]
public class MissileInitialBurnState : MissileBaseState
{
    private Vector3 direction;
    [SerializeField] private float initialBurnTime = 0.75f;
    private float counter = 0;

    public override void Enter()
    {
        counter = 0;
        direction = Missile.initialDirection;
    }

    public override void Run()
    {
        Missile.transform.Translate(direction * speed * Time.deltaTime);
        counter += Time.deltaTime;
        if(counter >= initialBurnTime)
        {
            stateMachine.TransitionTo<MissileApproachTargetState>();
        }
    }

    public override void Exit()
    {
        counter = 0;
    }
}
