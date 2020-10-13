using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTurret : Turret
{
    [SerializeField] private Transform missileSpawnPoint;
    [SerializeField] private Transform missileInitialBurnTarget;
    [SerializeField] private GameObject missilePrefab;
    private static ObjectPool<Missile> missilePool;

    private void Awake()
    {
        Initialize(25, 20, 0.125f);
        if(missilePool == null)
        {
            missilePool = new ObjectPool<Missile>(missilePrefab);
        }
    }

    private void Update()
    {
        stateMachine.Run();
    }

    public override void ReturnToObjectPool()
    {
        ObjectPooler.Instance.missileTurretPool.Add(this);
        if (constructed)
        {
            Base.RelieveCommand(commandCost);
            constructed = false;
        }
    }

    public static void ReturnMissileToObjectPool(Missile missile)
    {
        missilePool.Add(missile);
    }

    public override void BecomeSelected()
    {
        base.BecomeSelected();
        selectionText.text = "Missile Turret" + System.Environment.NewLine +
            "Fires homing missiles on enemies" + System.Environment.NewLine +
            "Deals " + AttackDamage + " explosive damage with each shot" + System.Environment.NewLine +
            "Medium rate of fire, low power draw" + System.Environment.NewLine +
            "Has an effective range of " + Range + " metres.";
    }

    public override void Shoot()
    {
        Missile missile = missilePool.Retrieve();
        missile.Initialize(missileInitialBurnTarget.position - missileSpawnPoint.transform.position, missileSpawnPoint.position, this, enemiesInRange[0]);
        PowerGrid.ConsumePower(PowerDraw);
    }
}
