using UnityEngine;

public class BallisticTurret : Turret
{
    [SerializeField] private Transform muzzle;
    [SerializeField] private GameObject bulletPrefab;
    private static ObjectPool<Bullet> bulletPool;

    private void Awake()
    {
        Initialize(50, 20, 0.1f);
        if(bulletPool == null) { bulletPool = new ObjectPool<Bullet>(bulletPrefab); }
    }

    private void Update()
    {
        stateMachine.Run();
    }

    public override void ReturnToObjectPool()
    {
        ObjectPooler.Instance.ballisticTurretPool.Add(this);
        if (constructed)
        {
            Base.RelieveCommand(commandCost);
            constructed = false;
        }
    }

    public override void BecomeSelected()
    {
        base.BecomeSelected();
        selectionText.text = "Ballistic Turret" + System.Environment.NewLine +
        "Fires Ballistic Slugs on enemies" + System.Environment.NewLine +
        "Deals " + AttackDamage + " Ballistic Damage with each shot" + System.Environment.NewLine +
        "Can fire 3 times before reloading" + System.Environment.NewLine +
        "Minimal power draw" + System.Environment.NewLine +
        "Has an effective range of " + Range + " metres.";
    }

    public static void ReturnBulletToPool(Bullet bullet) { bulletPool.Add(bullet); }

    public override void Shoot()
    {
        Bullet bullet = bulletPool.Retrieve();
        bullet.Initialize(muzzle.position, this, enemiesInRange[0]);
        PowerGrid.ConsumePower(PowerDraw);
    }
}
