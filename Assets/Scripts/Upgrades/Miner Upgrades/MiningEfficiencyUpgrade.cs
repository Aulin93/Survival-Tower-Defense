using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrade/Miner/Efficiency")]
public class MiningEfficiencyUpgrade : Upgrade, IUpgrade<Miner>
{
    [SerializeField] private float priceMultiplier;
    [SerializeField] private float efficiencyIncrease;
    [SerializeField] private Resource.ResourceType resourceType;
    private static Dictionary<Miner, Price> priceByMiner = new Dictionary<Miner, Price>();

    public void Upgrade(Miner miner)
    {
        if (!priceByMiner.ContainsKey(miner))
        {
            priceByMiner.Add(miner, Instantiate(InitialPrice));
            priceByMiner[miner].InitializeDictionary();
        }
        if (Inventory.CanAfford(priceByMiner[miner]))
        {
            miner.IncreaseMiningEfficiency(resourceType, efficiencyIncrease);
            Inventory.PayPrice(priceByMiner[miner]);
            priceByMiner[miner] *= priceMultiplier;
        }
    }
}
