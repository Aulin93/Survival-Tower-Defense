using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float range = 1;
    [SerializeField] private int damage = 1;
    [SerializeField] private ParticleSystem deathParticles;
    [SerializeField] private AudioClip deathSoundFX;
    private NavMeshAgent agent;

    private static Dictionary<Collider, Enemy> enemyByCollider = new Dictionary<Collider, Enemy>();

    public bool IsDead { get; private set; }

    protected Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
        agent = GetComponent<NavMeshAgent>();
        enemyByCollider.Add(GetComponent<Collider>(), this);
    }

    public void Initialize(Vector3 initialPosition)
    {
        //transform.position = initialPosition;
        agent.Warp(initialPosition);
        health.Initialize(100, new Vector3(1, 1, 1));
        IsDead = false;
        agent.SetDestination(Base.Position);
        agent.speed = speed;
    }

    private void Update()
    {
        if (IsDead)
        {
            agent.isStopped = true;
            ObjectPooler.Instance.enemyPool.Add(this);
        }
        else
        {
            Vector3 distanceToBase = Base.Position - transform.position;
            if(distanceToBase.sqrMagnitude <= range * range)
            {
                Base.LoseLives(damage);
                Die();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        EventCoordinator.FireEvent(new EnemyEnteredRangeEvent(other, this));
    }

    private void OnTriggerExit(Collider other)
    {
        EventCoordinator.FireEvent(new EnemyExitedRangeEvent(other, this));
    }

    /// <summary>
    /// Reduces hit points and returns the damage taken after resistance mitigation.
    /// </summary>
    /// <param name="damageValue">Amount of damage before resistance calculation.</param>
    /// <param name="damageType">The attack's damage type, damage will be mitigated depending on damage type.</param>
    /// <returns></returns>
    public float ReceiveDamage(float damageValue, Health.DamageType damageType)
    {
        float damageTaken = 0;
        if (health.hitPoints > 0)
        {
            damageTaken = health.TakeDamage(damageValue, damageType);
            if(health.hitPoints <= 0)
            {
                Die();
            }
        }
        return damageTaken;
    }

    private void Die()
    {
        IsDead = true;
        StartCoroutine(DeathSequence());
        EventCoordinator.FireEvent(new EnemyDiedEvent(this, deathParticles, deathSoundFX));
    }

    private IEnumerator DeathSequence()
    {
        yield return new WaitForSeconds(deathParticles.main.duration);
        ObjectPooler.Instance.enemyPool.Add(this);
    }

    public static Enemy GetEnemyFromCollider(Collider collider)
    {
        if (enemyByCollider.ContainsKey(collider))
        {
            return enemyByCollider[collider];
        }
        else
        {
            return null;
        }
    }
}
