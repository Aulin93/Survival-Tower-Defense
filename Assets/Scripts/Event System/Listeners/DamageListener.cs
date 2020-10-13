using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageListener : MonoBehaviour
{
    private void Awake()
    {
        EventCoordinator.RegisterListener<DamageEvent>(OnDamage);
    }

    public void OnDamage(DamageEvent damageEvent)
    {
        damageEvent.attacker.TotalUnmitigatedDamageDealt += damageEvent.damage;
        damageEvent.attacker.TotalMitigatedDamageDealt += damageEvent.target.ReceiveDamage(damageEvent.damage, damageEvent.damageType);
    }
}
