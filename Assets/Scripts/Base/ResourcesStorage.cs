using UnityEngine;

[RequireComponent(typeof(HarvesterCreatingState))]
[RequireComponent (typeof(NewBaseCreatingState))]
public class ResourcesStorage : MonoBehaviour
{
    private int _resourcesAmount;

    public IResourceSpenderState State { get; set; }    

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
