using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathListener : MonoBehaviour
{
    private Turret lastTurretToDealDamage;
    private int enemiesLeftToKill;

    private void Awake()
    {
        EventCoordinator.RegisterListener<DamageEvent>(OnDamage);
        EventCoordinator.RegisterListener<EnemyDiedEvent>(OnEnemyDeath);
        EventCoordinator.RegisterListener<WaveStartEvent>(OnWaveStart);
    }

    public void OnDamage(DamageEvent damageEvent)
    {
        lastTurretToDealDamage = damageEvent.attacker;
    }

    public void OnWaveStart(WaveStartEvent waveStart)
    {
        enemiesLeftToKill = waveStart.numberOfEnemiesInWave;
    }

    public void OnEnemyDeath(EnemyDiedEvent enemyDiedEvent)
    {
        enemiesLeftToKill--;
        if(enemiesLeftToKill <= 0) { EventCoordinator.FireEvent(new WaveClearedEvent()); }
    }

}
