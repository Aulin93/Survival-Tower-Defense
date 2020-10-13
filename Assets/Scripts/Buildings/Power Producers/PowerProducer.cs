using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerProducer : Building
{
    [SerializeField] protected float powerProducedPerSecond;

    public void ProducePower()
    {
        PowerGrid.StorePower(powerProducedPerSecond * Time.deltaTime);
    }

    public override void Construct()
    {
        stateMachine.TransitionTo<PowerProducerProductionState>();
        constructed = true;
    }

    public void IncreaseProduction(float productionIncrease)
    {
        powerProducedPerSecond += productionIncrease;
    }
}
