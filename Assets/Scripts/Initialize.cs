using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialize : MonoBehaviour
{
    [SerializeField] private Harvester _harvesterPrefab;

    private void Awake()
    {
        int harvestersAmount = 3;
        ResourcesManager storage = FindObjectOfType<ResourcesManager>();

        for (int i = 0; i < harvestersAmount; i++)
        {
            Harvester harvester = Instantiate(_harvesterPrefab, storage.transform.position, Quaternion.identity);
            harvester.SetStorage(storage);
            storage.AddHarvester(harvester);
        }
    }
}
