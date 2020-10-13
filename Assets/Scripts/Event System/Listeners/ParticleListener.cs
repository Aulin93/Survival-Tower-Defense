using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleListener : MonoBehaviour
{
    private void Awake()
    {
        EventCoordinator.RegisterListener<EnemyDiedEvent>(OnEnemyDeath);
    }

    public void OnEnemyDeath(EnemyDiedEvent enemyDiedEvent)
    {
        StartCoroutine(ManageParticles(enemyDiedEvent.deathParticles));
    }

    private IEnumerator ManageParticles(ParticleSystem particleSystem)
    {
        particleSystem.gameObject.SetActive(true);
        yield return new WaitForSeconds(particleSystem.main.duration);
        particleSystem.gameObject.SetActive(false);
    }
}
