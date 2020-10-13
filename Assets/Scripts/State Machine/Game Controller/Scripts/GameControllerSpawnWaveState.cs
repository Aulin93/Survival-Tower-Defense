using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameControllerState/SpawnWaveState")]
public class GameControllerSpawnWaveState : GameControllerBaseState
{
    [SerializeField] private int enemiesToSpawn = 20;
    [SerializeField] private int numberOfSpawnPointsToUse = 1;
    [SerializeField] private int waveMultiplier = 2;
    [SerializeField] private float timeBetweenSpawns = 0.5f;
    private int spawnedEnemies;
    private float counter;
    private Vector3[] spawnPointsToUse;

    public override void Enter()
    {
        spawnedEnemies = 0;
        counter = 0;
        EventCoordinator.FireEvent(new WaveStartEvent(enemiesToSpawn * numberOfSpawnPointsToUse));
        spawnPointsToUse = new Vector3[numberOfSpawnPointsToUse];
        for (int i = 0; i < numberOfSpawnPointsToUse; i++)
        {
            spawnPointsToUse[i] = SpawnPoint.spawnPoints[0];
            for (int j = 0; j < SpawnPoint.spawnPoints.Count; j++)
            {
                Vector3 distanceToBase = Base.Position - SpawnPoint.spawnPoints[j];
                Vector3 shortestDistanceToBase = Base.Position - spawnPointsToUse[i];
                if(distanceToBase.sqrMagnitude < shortestDistanceToBase.sqrMagnitude) { spawnPointsToUse[i] = SpawnPoint.spawnPoints[j]; }
            }
            SpawnPoint.ExcludeSpawnPoint(spawnPointsToUse[i]);
        }
    }

    public override void Run()
    {
        counter += Time.deltaTime;
        if(counter >= timeBetweenSpawns)
        {
            for (int i = 0; i < spawnPointsToUse.Length; i++)
            {
                GameController.enemyPool.Retrieve().Initialize(spawnPointsToUse[i]);
            }
            spawnedEnemies++;
            counter = 0;
            if(spawnedEnemies >= enemiesToSpawn) { stateMachine.TransitionTo<GameControllerIdleState>(); }
        }
    }

    public override void Exit()
    {
        SpawnPoint.RefreshList();
        enemiesToSpawn *= waveMultiplier;
        if(numberOfSpawnPointsToUse < 3) { numberOfSpawnPointsToUse++; }
    }
}
