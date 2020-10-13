using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarPanels : PowerProducer
{
    private void Awake()
    {
        commandCost = 1;
        powerProducedPerSecond = 1;
        InitializeStateMachine();
        InitializePrices();
        RegisterListeners();
        GameController.AddSelectableCollider(GetComponent<BoxCollider>(), this);
    }

    void Update()
    {
        stateMachine.Run();
    }

    public override void ReturnToObjectPool()
    {
        ObjectPooler.Instance.solarPanelsPool.Add(this);
        if (constructed)
        {
            Base.RelieveCommand(commandCost);
            constructed = false;
        }
    }

    public override void BecomeSelected()
    {
        base.BecomeSelected();
        selectionText.text = "Solar Panels" + System.Environment.NewLine +
            "Produces " + powerProducedPerSecond + " power per second.";
    }
}
