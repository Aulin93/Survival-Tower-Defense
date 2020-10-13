using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public Vector3 initialDirection { get; private set; }
    public Turret launcher { get; private set; }

    private Enemy target;
    [SerializeField] private float detonationRadius = 1;
    private StateMachine stateMachine;
    [SerializeField] private State[] states;

    public void Initialize(Vector3 direction, Vector3 initialPosition, Turret missileLauncher, Enemy targetEnemy)
    {
        transform.position = initialPosition;
        initialDirection = direction.normalized;
        launcher = missileLauncher;
        target = targetEnemy;
        stateMachine = new StateMachine(this, states);
        stateMachine.TransitionTo<MissileInitialBurnState>();
    }

    private void Update()
    {
        stateMachine.Run();
    }

    public void Detonate()
    {
        Collider[] blastedObjects = Physics.OverlapSphere(transform.position, detonationRadius);
        if(blastedObjects.Length > 0)
        {
            for (int i = 0; i < blastedObjects.Length; i++)
            {
                Enemy blastedEnemy = Enemy.GetEnemyFromCollider(blastedObjects[i]);
                if(blastedEnemy != null) {
                    EventCoordinator.FireEvent(new DamageEvent(launcher, blastedEnemy, launcher.AttackDamage, Health.DamageType.Explosive));
                }
            }
        }
        MissileTurret.ReturnMissileToObjectPool(this);
    }

    public Vector3 GetDirectionToTarget()
    {
        Vector3 directionToTarget = target.transform.position - transform.position;
        return directionToTarget;
    }

    public float GetDetonationRadius() { return detonationRadius; }
}
