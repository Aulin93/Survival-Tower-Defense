using System.Collections.Generic;
using UnityEngine;

public abstract class Turret : Building
{
    public float Range { get; protected set; }
    public float AttackDamage { get; set; }
    public float PowerDraw { get; set; }
    public float TotalUnmitigatedDamageDealt { get; set; }
    public float TotalMitigatedDamageDealt { get; set; }
    public int TotalKills { get; set; }

    [SerializeField] protected Price clipPrice;
    protected List<Enemy> enemiesInRange = new List<Enemy>();
    [SerializeField] protected SphereCollider rangeCollider;

    protected static Dictionary<Collider, Turret> turretByCollider = new Dictionary<Collider, Turret>();

    public virtual void Initialize(float initialAttackDamage, float initialRange, float initalPowerDraw)
    {
        commandCost = 5;
        GameController.AddSelectableCollider(GetComponent<SphereCollider>(), this);
        PowerDraw = initalPowerDraw;
        AttackDamage = initialAttackDamage;
        Range = initialRange;
        rangeCollider.radius = Range;
        turretByCollider.Add(rangeCollider, this);
        EventCoordinator.RegisterListener<EnemyDiedEvent>(OnEnemyDied);
        InitializeStateMachine();
        InitializePrices();
        RegisterListeners();
    }

    public override void Construct()
    {
        stateMachine.TransitionTo<TurretFiringState>();
        constructed = true;
    }

    public override void InitializePrices()
    {
        base.InitializePrices();
        clipPrice = Instantiate(clipPrice);
        clipPrice.InitializeDictionary();
    }

    public void AddEnemyInRange(Enemy enemy)
    {
        if (!enemiesInRange.Contains(enemy)) { enemiesInRange.Add(enemy); }
    }

    public void RemoveEscapedEnemy(Enemy enemy) { enemiesInRange.Remove(enemy); }

    public void OnEnemyDied(EnemyDiedEvent enemyDiedEvent)
    {
        enemiesInRange.Remove(enemyDiedEvent.killedEnemy);
    }

    public abstract void Shoot();

    public void IncreaseRange(float rangeIncrease)
    {
        Range += rangeIncrease;
        rangeCollider.radius = Range;
    }

    public bool HasEnemyInRange() { return enemiesInRange.Count > 0; }
    public Price GetClipPrice() { return clipPrice; }

    public static Turret GetTurretFromCollider(Collider collider)
    {
        if (turretByCollider.ContainsKey(collider))
        {
            return turretByCollider[collider];
        }
        else
        {
            return null;
        }
    }
}
