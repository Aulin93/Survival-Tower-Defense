using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TurretState/ReloadState")]
public class TurretReloadState : TurretBaseState
{
    private bool hasPurchasedClip;
    private float counter = 0;
    [SerializeField] private float reloadTime = 1;

    public override void Enter()
    {
        counter = 0;
        hasPurchasedClip = false;
    }

    public override void Run()
    {
        if (!hasPurchasedClip)
        {
            if (Inventory.CanAfford(Turret.GetClipPrice()))
            {
                Inventory.PayPrice(Turret.GetClipPrice());
                hasPurchasedClip = true;
            }
        }
        else
        {
            counter += Time.deltaTime;
            if (counter >= reloadTime)
            {
                stateMachine.TransitionTo<TurretFiringState>();
            }
        }
    }
}
