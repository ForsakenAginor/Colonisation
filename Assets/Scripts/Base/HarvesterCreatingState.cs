using UnityEngine;

[RequireComponent (typeof(ResourcesStorage))]
public class HarvesterCreatingState : MonoBehaviour, IResourceSpenderState
{
    [SerializeField] private HarvesterWorkerState _harvesterPrefab;

    private ResourcesStorage _storage;
    private HarvestManager _harvestManager;
    
    public void SpendResources()
    {
        int spawnCost = 3;

        if (_storage.TrySpendResources(spawnCost))
        {
            HarvesterWorkerState harvester = Instantiate(_harvesterPrefab, _storage.transform.position, Quaternion.identity);
            _harvestManager.AddHarvester(harvester);
            harvester.SetStorage(_harvestManager);
        }
    }

    private void Awake()
    {
        _storage = GetComponent<ResourcesStorage>();
        _harvestManager = GetComponent<HarvestManager>();
    }
}
