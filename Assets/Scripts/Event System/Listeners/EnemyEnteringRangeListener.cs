using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEnteringRangeListener : TurretRangeListener
{
    private void Awake()
    {
        EventCoordinator.RegisterListener<EnemyEnteredRangeEvent>(OnEnemyEnteredRange);
    }

    public void OnEnemyEnteredRange(EnemyEnteredRangeEvent enemyEnteredRangeEvent)
    {
        Turret turret = Turret.GetTurretFromCollider(enemyEnteredRangeEvent.turretRange);
        if(turret != null) { turret.AddEnemyInRange(enemyEnteredRangeEvent.enemy); }
    }
}
