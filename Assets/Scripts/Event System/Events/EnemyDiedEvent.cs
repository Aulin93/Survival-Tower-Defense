using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDiedEvent : CustomEvent
{
    public Enemy killedEnemy;
    public ParticleSystem deathParticles;
    public AudioClip deathSoundFX;

    public EnemyDiedEvent(Enemy enemy, ParticleSystem deathFX, AudioClip deathSFX) : base("This is an Enemy Has Died Event") {
        killedEnemy = enemy;
        deathParticles = deathFX;
        deathSoundFX = deathSFX;
    }
}
