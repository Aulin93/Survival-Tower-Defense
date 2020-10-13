using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TurretState/FiringState")]
public class TurretFiringState : TurretBaseState
{
    [SerializeField] private float rateOfFire = 0.25f;
    [SerializeField] private int clipSize = 5;
    private float counter = 0;
    private int roundsInCurrentClip;

    public override void Enter()
    {
        roundsInCurrentClip = clipSize;
        counter = rateOfFire;
    }

    public override void Run()
    {
        counter += Time.deltaTime;
        if(counter >= rateOfFire && Turret.HasEnemyInRange() && PowerGrid.PowerIsAvailable(Turret.PowerDraw))
        {
            counter = 0;
            Turret.Shoot();
            roundsInCurrentClip--;
            if(roundsInCurrentClip <= 0)
            {
                stateMachine.TransitionTo<TurretReloadState>();
            }
        }
    }

    public override void Exit()
    {
        counter = 0;
    }
}
