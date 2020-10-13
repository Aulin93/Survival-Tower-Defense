using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerProducerBaseState : BuildingBaseState
{
    private PowerProducer powerProducerController;

    public PowerProducer PowerProducer => powerProducerController = powerProducerController ?? (PowerProducer)stateMachine.owner;
}
