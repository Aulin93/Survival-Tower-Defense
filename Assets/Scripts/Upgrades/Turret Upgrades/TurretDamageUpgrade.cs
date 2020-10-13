using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrade/Turret/Damage")]
public class TurretDamageUpgrade : Upgrade, IUpgrade<Turret>
{
    private static Dictionary<Turret, Price> priceByTurret = new Dictionary<Turret, Price>();
    [SerializeField] private float flatDamageModifier;
    [SerializeField] private float damageMultiplier;
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
            turret.AttackDamage = (turret.AttackDamage + flatDamageModifier) * damageMultiplier;
            Inventory.PayPrice(priceByTurret[turret]);
            priceByTurret[turret] *= priceMultiplier;
        }
    }
}
