using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResourceSpawner : Spawner
{
    [SerializeField] private int _starterResources;
    [SerializeField] private UnityEvent _spawned;

    private float _spawnFrequency = 5;

    private void Start()
    {
        Spawn(_starterResources);
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
            _spawned?.Invoke();

            yield return spawnDelay;
        }
    }
}
