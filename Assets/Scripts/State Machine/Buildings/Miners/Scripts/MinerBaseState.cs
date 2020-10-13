using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MinerBaseState : State
{
    private Miner controller;

    public Miner Miner => controller = controller ?? (Miner)stateMachine.owner;
}
