using UnityEngine;

public class EnemyEnteredRangeEvent : CustomEvent
{
    public Collider turretRange;
    public Enemy enemy;

    public EnemyEnteredRangeEvent(Collider turretCollider, Enemy enteringEnemy):base("This is an Enemy Entered Turret's Range Event")
    {
        turretRange = turretCollider;
        enemy = enteringEnemy;
    }
}
