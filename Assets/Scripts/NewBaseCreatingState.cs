using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBaseCreatingState : MonoBehaviour, IResourceSpenderState
{
    [SerializeField] private Transform _basePrefab;
    [SerializeField] private HarvesterCreatingState _harvesterCreatingState;

    private Vector3 _position;
    private ResourcesManager _storage;

    public void SpendResources()
    {
        int spawnCost = 5;

        if (_storage.TrySpendResources(spawnCost))
        {
            Transform newBase = Instantiate(_basePrefab, _position, Quaternion.identity);

            if (_storage.TryGetHarvester(out Harvester harvester))
            {
                ResourcesManager newBaseStorage = newBase.GetComponent<ResourcesManager>();
                newBaseStorage.AddHarvester(harvester);
                harvester.SetStorage(newBaseStorage);
                harvester.SetNewBase(newBase);
            }

            _storage.State = _harvesterCreatingState;
        }
    }

    public void SetBuildingPosition(Vector3 point)
    {
        _position = point;
        _storage.State = this;
    }

    private void Awake()
    {
        _storage = GetComponent<ResourcesManager>();
    }
}
