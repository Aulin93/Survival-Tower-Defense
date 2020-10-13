using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject ballisticTurretPrefab;
    [SerializeField] private GameObject energyTurretPrefab;
    [SerializeField] private GameObject missileTurretPrefab;
    [SerializeField] private GameObject solarPanelsPrefab;
    [SerializeField] private GameObject windTurbinePrefab;
    [SerializeField] private GameObject minerPrefab;
    [SerializeField] private GameObject enemyPrefab;

    public static ObjectPooler Instance { get; private set; }

    public ObjectPool<BallisticTurret> ballisticTurretPool { get; private set; }
    public ObjectPool<EnergyTurret> energyTurretPool { get; private set; }
    public ObjectPool<MissileTurret> missileTurretPool { get; private set; }
    public ObjectPool<SolarPanels> solarPanelsPool { get; private set; }
    public ObjectPool<WindTurbine> windTurbinePool { get; private set; }
    public ObjectPool<Miner> minerPool { get; private set; }
    public ObjectPool<Enemy> enemyPool { get; private set; }

    private void Awake()
    {
        ballisticTurretPool = new ObjectPool<BallisticTurret>(ballisticTurretPrefab);
        energyTurretPool = new ObjectPool<EnergyTurret>(energyTurretPrefab);
        missileTurretPool = new ObjectPool<MissileTurret>(missileTurretPrefab);
        solarPanelsPool = new ObjectPool<SolarPanels>(solarPanelsPrefab);
        windTurbinePool = new ObjectPool<WindTurbine>(windTurbinePrefab);
        minerPool = new ObjectPool<Miner>(minerPrefab);
        enemyPool = new ObjectPool<Enemy>(enemyPrefab);
        Instance = this;
    }
}
