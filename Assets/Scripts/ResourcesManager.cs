using System.Linq;
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(HarvesterCreatingState))]
[RequireComponent (typeof(NewBaseCreatingState))]
public class ResourcesManager : MonoBehaviour
{
    private List<Harvester> _harvesters;
    private int _resourcesAmount;

    public IResourceSpenderState State { get; set; }

    public List<Harvester> GetAvailableHarvesters()
    {
        return new List<Harvester>(_harvesters);
    }

    public void AddHarvester(Harvester harvester)
    {
        if (_harvesters == null)
        {
            _harvesters = new List<Harvester>();
        }

        _harvesters.Add(harvester);
    }

    public bool TryGetHarvester(out Harvester harvester)
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

    public void AddResource()
    {
        _resourcesAmount++;
    }

    public bool TrySpendResources(int amount)
    {
        if (amount <= _resourcesAmount)
        {
            _resourcesAmount -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Awake()
    {
        State = GetComponent<HarvesterCreatingState>();
    }

    private void Update()
    {
        State.SpendResources();
    }
}
