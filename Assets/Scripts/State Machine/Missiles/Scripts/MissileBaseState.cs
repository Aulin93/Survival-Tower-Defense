using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBaseState : State
{
    private Missile controller;

    public Missile Missile => controller = controller ?? (Missile)stateMachine.owner;

    [SerializeField] protected float speed = 20f;
}
