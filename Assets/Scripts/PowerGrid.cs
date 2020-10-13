using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PowerGrid
{
    public static float capacity { get; private set; }
    public static float storedPower { get; private set; }

    public static void Initialize(float initialCapacity)
    {
        capacity = initialCapacity;
        storedPower = capacity;
        EventCoordinator.RegisterListener<WaveClearedEvent>(OnWaveCleared);
    }

    public static void StorePower(float powerProduced)
    {
        storedPower += powerProduced;
        if(storedPower > capacity) { storedPower = capacity; }
    }

    public static void ConsumePower(float powerConsumed)
    {
        storedPower -= powerConsumed;
        if(storedPower < 0)
        {
            Debug.LogError("Consumed more power than was available");
            storedPower = 0;
        }
    }

    public static void IncreaseCapacity(float additionalCapacity) { capacity += additionalCapacity; }

    public static void DecreaseCapacity(float lostCapacity)
    {
        capacity -= lostCapacity;
        if(capacity < 0)
        {
            Debug.LogError("Power Grid reduced to less than 0 capacity");
            capacity = 0;
        }
    }

    public static void OnWaveCleared(WaveClearedEvent waveCleared)
    {
        storedPower = capacity;
    }

    public static bool PowerIsAvailable(float requestedPower) { return requestedPower <= storedPower; }
}
