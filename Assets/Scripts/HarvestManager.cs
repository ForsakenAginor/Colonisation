using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HarvestManager : MonoBehaviour
{
    private ResourcesManager _workerManager;
    private List<Resource> _harvestableResources;

    private void Awake()
    {
        _workerManager = GetComponent<ResourcesManager>();
    }

    private void Update()
    {
        FindResourcesForHarvest();

        foreach (Harvester harvester in _workerManager.GetAvailableHarvesters())
        {
            if (harvester.IsBusy == false && _harvestableResources.Count > 0)
            {
                harvester.SetTarget(_harvestableResources[0].transform);
                _harvestableResources[0].IsMarkedForHarvest = true;
                _harvestableResources.RemoveAt(0);
            }
        }
    }

    private void FindResourcesForHarvest()
    {
        _harvestableResources = FindObjectsByType<Resource>(FindObjectsSortMode.None).
            Where(resource => resource.IsMarkedForHarvest == false).
            OrderBy(resource => (resource.transform.position - transform.position).magnitude).
            ToList();
    }
}
