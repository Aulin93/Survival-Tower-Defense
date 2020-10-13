using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerProducerState/ProductionState")]
public class PowerProducerProductionState : PowerProducerBaseState
{
    public override void Run()
    {
        PowerProducer.ProducePower();
    }
}
