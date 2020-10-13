using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrade/Power Producer/Production")]
public class PowerProductionUpgrade : Upgrade, IUpgrade<PowerProducer>
{
    [SerializeField] private float productionModifier;
    [SerializeField] private float priceMultiplier;
    private static Dictionary<PowerProducer, Price> powerProducerByPrice = new Dictionary<PowerProducer, Price>();

    public void Upgrade(PowerProducer powerProducer)
    {
        if (!powerProducerByPrice.ContainsKey(powerProducer))
        {
            powerProducerByPrice.Add(powerProducer, Instantiate(InitialPrice));
            powerProducerByPrice[powerProducer].InitializeDictionary();
        }
        if (Inventory.CanAfford(powerProducerByPrice[powerProducer]))
        {
            powerProducer.IncreaseProduction(productionModifier);
            Inventory.PayPrice(powerProducerByPrice[powerProducer]);
            powerProducerByPrice[powerProducer] *= priceMultiplier;
        }
    }
}
