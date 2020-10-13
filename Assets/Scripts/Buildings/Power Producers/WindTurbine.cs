using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTurbine : PowerProducer
{
    private void Awake()
    {
        commandCost = 1;
        GameController.AddSelectableCollider(GetComponent<CapsuleCollider>(), this);
        powerProducedPerSecond = 2;
        InitializeStateMachine();
        InitializePrices();
        RegisterListeners();
    }

    void Update()
    {
        stateMachine.Run();
    }

    public override void ReturnToObjectPool()
    {
        ObjectPooler.Instance.windTurbinePool.Add(this);
        if (constructed)
        {
            Base.RelieveCommand(commandCost);
            constructed = false;
        }
    }

    public override void BecomeSelected()
    {
        base.BecomeSelected();
        selectionText.text = "Wind Turbine" + System.Environment.NewLine +
            "Produces " + powerProducedPerSecond + " power per second.";
    }
}
