

public class DamageEvent : CustomEvent
{
    public Turret attacker;
    public Enemy target;
    public float damage;
    public Health.DamageType damageType;

    public DamageEvent(Turret attackingTurret, Enemy targetEnemy, float damageValue, Health.DamageType type):base("This is a Damage Event")
    {
        attacker = attackingTurret;
        target = targetEnemy;
        damage = damageValue;
        damageType = type;
    }
}
