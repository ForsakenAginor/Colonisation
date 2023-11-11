using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HarvesterCreatingState : MonoBehaviour, IResourceSpenderState
{
    [SerializeField] private Harvester _harvesterPrefab;

    private ResourcesManager _storage;
    
    public void SpendResources()
    {
        int spawnCost = 3;

        if (_storage.TrySpendResources(spawnCost))
        {
            Harvester harvester = Instantiate(_harvesterPrefab, _storage.transform.position, Quaternion.identity);
            _storage.AddHarvester(harvester);
            harvester.SetStorage(_storage);
        }
    }

    private void Awake()
    {
        _storage = GetComponent<ResourcesManager>();
    }
}
