using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundListener : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        EventCoordinator.RegisterListener<EnemyDiedEvent>(OnEnemyDeath);
    }

    public void OnEnemyDeath(EnemyDiedEvent enemyDiedEvent)
    {
        audioSource.PlayOneShot(enemyDiedEvent.deathSoundFX);
    }
}
