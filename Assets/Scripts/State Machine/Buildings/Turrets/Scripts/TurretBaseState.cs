using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretBaseState : State
{
    private Turret controller;

    public Turret Turret => controller = controller ?? (Turret)stateMachine.owner;
}
