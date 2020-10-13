using UnityEngine;

public class EnemyExitedRangeEvent : CustomEvent
{
    public Collider turretRange;
    public Enemy escapedEnemy;

    public EnemyExitedRangeEvent(Collider collider, Enemy enemy) : base("This is an Enemy Has Exited Turret Range Event")
    {
        turretRange = collider;
        escapedEnemy = enemy;
    }
}
