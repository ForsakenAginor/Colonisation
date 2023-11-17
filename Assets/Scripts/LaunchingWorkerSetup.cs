using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchingWorkerSetup : MonoBehaviour
{
    [SerializeField] private HarvesterWorkerState _harvesterPrefab;
    [SerializeField] private int _starterHarvesters;

    private void Awake()
    {
        HarvestManager storage = FindObjectOfType<HarvestManager>();

        for (int i = 0; i < _starterHarvesters; i++)
        {
            HarvesterWorkerState harvester = Instantiate(_harvesterPrefab, storage.transform.position, Quaternion.identity);
            harvester.SetStorage(storage);
            storage.AddHarvester(harvester);
        }
    }
}
