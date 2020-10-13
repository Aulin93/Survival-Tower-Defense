using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingBaseState : State
{
    private Building controller;

    public Building Building => controller = controller ?? (Building)stateMachine.owner;
}
