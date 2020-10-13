using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Turret shooter;
    private Enemy target;
    [SerializeField] private float speed = 30;

    public void Initialize(Vector3 initialPosition, Turret shootingTurret, Enemy targetEnemy)
    {
        transform.position = initialPosition;
        shooter = shootingTurret;
        target = targetEnemy;
    }

    private void Update()
    {
        Vector3 direction = target.transform.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime);
        if(Vector3.Distance(transform.position, target.transform.position) <= 0.05f)
        {
            EventCoordinator.FireEvent(new DamageEvent(shooter, target, shooter.AttackDamage, Health.DamageType.Ballistic));
            BallisticTurret.ReturnBulletToPool(this);
        }
    }
}
