using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public static List<Vector3> spawnPoints = new List<Vector3>();
    private static List<Vector3> excludedPoints = new List<Vector3>();

    private void Awake()
    {
        spawnPoints.Add(transform.position);
        Destroy(gameObject);
    }

    public static void ExcludeSpawnPoint(Vector3 excludedPoint)
    {
        if (spawnPoints.Contains(excludedPoint))
        {
            spawnPoints.Remove(excludedPoint);
            excludedPoints.Add(excludedPoint);
        }
    }

    public static void RefreshList()
    {
        for (int i = 0; i < excludedPoints.Count; i++)
        {
            spawnPoints.Add(excludedPoints[i]);
        }
        excludedPoints.Clear();
    }
}
