using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrade/Turret/Range")]
public class TurretRangeUpgrade : Upgrade, IUpgrade<Turret>
{
    private static Dictionary<Turret, Price> priceByTurret = new Dictionary<Turret, Price>();
    [SerializeField] private float rangeModifier;
    [SerializeField] private float priceMultiplier;

    public void Upgrade(Turret turret)
    {
        if (!priceByTurret.ContainsKey(turret))
        {
            priceByTurret.Add(turret, Instantiate(InitialPrice));
            priceByTurret[turret].InitializeDictionary();
        }
        if (Inventory.CanAfford(priceByTurret[turret]))
        {
            turret.IncreaseRange(rangeModifier);
            Inventory.PayPrice(priceByTurret[turret]);
            priceByTurret[turret] *= priceMultiplier;
        }
    }
}
