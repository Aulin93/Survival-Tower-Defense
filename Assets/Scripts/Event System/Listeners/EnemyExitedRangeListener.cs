using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExitedRangeListener : TurretRangeListener
{
    public void OnEnemyExitedRange(EnemyExitedRangeEvent enemyExitedRangeEvent)
    {
        Turret turret = Turret.GetTurretFromCollider(enemyExitedRangeEvent.turretRange);
        if (turret != null) { turret.RemoveEscapedEnemy(enemyExitedRangeEvent.escapedEnemy); }
    }
}
