using System.Linq;
using UnityEngine;

[RequireComponent(typeof(HarvestManager))]
public class ResourcesManager : MonoBehaviour
{
    private HarvestManager _harvestManager;
    private Harvester[] _harvesters;
    private Spawner _spawner;
    private int _resourceCount;
    private int _initialHarvestersQuantity = 3;

    public State State { get; set; }

    public Harvester[] GetAvailableHarvesters()
    {
        Harvester[] harvesters = new Harvester[_harvesters.Length];

        for (int i = 0; i <_harvesters.Length; i++)
        {
            harvesters[i] = _harvesters[i];
        }

        return harvesters;
    }

    public void AddResource()
    {
        _resourceCount++;
    }

    private void Awake()
    {
        _spawner = GetComponent<Spawner>();
        _harvestManager = GetComponent<HarvestManager>();
        _harvesters = _spawner.Spawn(_initialHarvestersQuantity).Select(harvester => harvester.GetComponent<Harvester>()).ToArray();

        foreach (Harvester harvester in _harvesters)
            harvester.SetStorage(_harvestManager);
    }

    private void Update()
    {
        SpawnAdditionalHarvester();
    }

    private void SpawnAdditionalHarvester()
    {
        int spawnCost = 3;

        if (_resourceCount >= spawnCost)
        {
            _harvesters = _harvesters.Union(_spawner.Spawn(1).Select(harvester => harvester.GetComponent<Harvester>())).ToArray();
            _harvesters[_harvesters.Length - 1].SetStorage(_harvestManager);
            _resourceCount -= spawnCost;
        }
    }
}
