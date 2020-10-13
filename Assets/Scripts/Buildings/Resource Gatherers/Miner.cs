using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : Building
{
    [SerializeField] private Resource.ResourceType[] mineableResources;
    [SerializeField] private float[] resourcesMinedPerSecond;
    [SerializeField] private float powerDraw;
    private Dictionary<Resource.ResourceType, float> miningEfficiency = new Dictionary<Resource.ResourceType, float>();
    private List<Resource> resourcesInRange = new List<Resource>();

    private void Awake()
    {
        commandCost = 3;
        for (int i = 0; i < mineableResources.Length; i++)
        {
            miningEfficiency.Add(mineableResources[i], resourcesMinedPerSecond[i]);
        }
        GameController.AddSelectableCollider(GetComponent<CapsuleCollider>(), this);
        InitializeStateMachine();
        InitializePrices();
        RegisterListeners();
    }

    private void Update()
    {
        stateMachine.Run();
    }

    public override void Construct()
    {
        stateMachine.TransitionTo<MinerMiningState>();
        constructed = true;
    }

    public override void ReturnToObjectPool()
    {
        ObjectPooler.Instance.minerPool.Add(this);
        if (constructed)
        {
            Base.RelieveCommand(commandCost);
            constructed = false;
        }
    }

    public void ScanForResources(float range)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        for (int i = 0; i < colliders.Length; i++)
        {
            Resource resource = colliders[i].GetComponent<Resource>();
            if(resource != null)
            {
                if(miningEfficiency.ContainsKey(resource.GetResourceType()))
                resourcesInRange.Add(resource);
            }
        }
    }

    public override void BecomeSelected()
    {
        base.BecomeSelected();
        selectionText.text = "Miner" + System.Environment.NewLine +
            "Mines metals with varying efficiency" + System.Environment.NewLine +
            "Iron: " + miningEfficiency[Resource.ResourceType.Iron] + "/sec" + System.Environment.NewLine +
            "Copper: " + miningEfficiency[Resource.ResourceType.Copper] + "/sec" + System.Environment.NewLine +
            "Aluminum: " + miningEfficiency[Resource.ResourceType.Aluminum] + "/sec" + System.Environment.NewLine +
            "Lead: " + miningEfficiency[Resource.ResourceType.Lead] + "/sec";
    }

    public void Mine()
    {
        if(resourcesInRange.Count > 0)
        {
            for (int i = 0; i < resourcesInRange.Count; i++)
            {
                Resource.ResourceType resourceType = resourcesInRange[i].GetResourceType();
                float amountOfResourceMined = miningEfficiency[resourceType] * Time.deltaTime;
                Inventory.AddResourceToInventory(resourceType, amountOfResourceMined);
            }
            PowerGrid.ConsumePower(powerDraw * Time.deltaTime);
        }
    }

    public void IncreaseMiningEfficiency(Resource.ResourceType resourceType, float efficiencyIncrease) { miningEfficiency[resourceType] += efficiencyIncrease; }

    public float GetPowerDraw() { return powerDraw; }
}
