using System.Collections.Generic;
using UnityEngine;

public class HarvestManager : MonoBehaviour
{
    private List<HarvesterWorkerState> _harvesters;
    private ResourcesList _resources;

    public void AddHarvester(HarvesterWorkerState harvester)
    {
        if (_harvesters == null)
        {
            _harvesters = new List<HarvesterWorkerState>();
        }

        _harvesters.Add(harvester);
    }

    public bool TryGetHarvester(out HarvesterWorkerState harvester)
    {
        if (_harvesters != null && _harvesters.Count > 0)
        {
            harvester = _harvesters[0];
            _harvesters.RemoveAt(0);
            return true;
        }
        else
        {
            harvester = null;
            return false;
        }
    }

    private void Awake()
    {
        _resources = FindObjectOfType<ResourcesList>();
    }

    private void Update()
    {
        SendHarvesters();
    }

    private void SendHarvesters()
    {
        foreach (var harvester in _harvesters)
        {
            if (harvester.IsBusy == false && _resources.TryGetClosestResource(harvester.transform, out Resource resource))
            {
                harvester.SetTarget(resource.transform);
                resource.IsMarkedForHarvest = true;
            }
        }
    }
}
