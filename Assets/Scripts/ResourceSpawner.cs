using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : Spawner
{
    [SerializeField] private int _amount;

    private float _spawnFrequency = 5;

    private void Start()
    {
        Spawn(_amount);
        StartCoroutine(SpawnResources());
    }

    private IEnumerator SpawnResources()
    {
        bool isSpawning = true;
        WaitForSeconds spawnDelay;
        int resourcesAmount = 1;

        while (isSpawning)
        {
            spawnDelay = new WaitForSeconds(_spawnFrequency);
            Spawn(resourcesAmount);

            yield return spawnDelay;
        }
    }
}
