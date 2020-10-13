using UnityEngine;

public class EnergyTurret : Turret
{
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private Transform muzzle;
    private GameObject spawnedLaser;
    private LineRenderer laserRenderer;

    private void Awake()
    {
        Initialize(10, 20, 1);
    }

    void Update()
    {
        if(laserRenderer != null && HasEnemyInRange())
        {
            laserRenderer.SetPosition(1, enemiesInRange[0].transform.position);
        }
        stateMachine.Run();
    }

    public override void ReturnToObjectPool()
    {
        ObjectPooler.Instance.energyTurretPool.Add(this);
        if (constructed)
        {
            Base.RelieveCommand(commandCost);
            constructed = false;
        }
    }

    public override void BecomeSelected()
    {
        base.BecomeSelected();
        selectionText.text = "Energy Turret" + System.Environment.NewLine +
            "Fires a sustained laser on enemies" + System.Environment.NewLine +
            "Deals " + AttackDamage + " energy damage with each shot" + System.Environment.NewLine +
            "High rate of fire and power draw" + System.Environment.NewLine +
            "Has an effective range of " + Range + " metres.";
    }

    public override void Shoot()
    {
        if(spawnedLaser == null) { FireLaser(); }
        EventCoordinator.FireEvent(new DamageEvent(this, enemiesInRange[0], AttackDamage, Health.DamageType.Energy));
        PowerGrid.ConsumePower(PowerDraw);
    }

    private void FireLaser()
    {
        spawnedLaser = Instantiate(laserPrefab, Vector3.zero, Quaternion.identity);
        laserRenderer = spawnedLaser.GetComponentInChildren<LineRenderer>();
        laserRenderer.SetPosition(0, muzzle.position);
    }
}
