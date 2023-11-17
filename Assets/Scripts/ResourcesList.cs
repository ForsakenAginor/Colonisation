using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourcesList : MonoBehaviour
{
    private List<Resource> _harvestableResources;

    public bool TryGetClosestResource(Transform harvester, out Resource resource)
    {
        if (_harvestableResources != null && _harvestableResources.Count > 0)
        {
            resource = _harvestableResources.
                Where(resource => resource.IsMarkedForHarvest == false).
                OrderBy(resource => (resource.transform.position - harvester.position).magnitude).
                First();
            _harvestableResources.Remove(resource);

            return true;
        }
        else
        {
            resource = null;
            return false;
        }
    }

    public void FindResourcesForHarvest()
    {
        _harvestableResources = FindObjectsByType<Resource>(FindObjectsSortMode.None).
            Where(resource => resource.IsMarkedForHarvest == false).ToList();
    }
}
