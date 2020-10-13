using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MissileState/ApproachTargetState")]
public class MissileApproachTargetState : MissileBaseState
{

    public override void Run()
    {
        Vector3 directionToTarget = Missile.GetDirectionToTarget().normalized;
        Missile.transform.Translate(directionToTarget * speed * Time.deltaTime);
        if(Missile.GetDirectionToTarget().sqrMagnitude <= Missile.GetDetonationRadius() * Missile.GetDetonationRadius() * 0.5f)
        {
            Missile.Detonate();
        }
    }
}
