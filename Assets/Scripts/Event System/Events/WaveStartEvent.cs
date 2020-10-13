using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveStartEvent : CustomEvent
{
    public int numberOfEnemiesInWave;

    public WaveStartEvent(int enemiesInWave) : base("This is a wave started event.")
    {
        numberOfEnemiesInWave = enemiesInWave;
    }
}
