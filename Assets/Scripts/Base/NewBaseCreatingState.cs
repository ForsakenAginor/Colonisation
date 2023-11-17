using UnityEngine;

[RequireComponent(typeof(ResourcesStorage))]
[RequireComponent (typeof(HarvestManager))]
public class NewBaseCreatingState : MonoBehaviour, IResourceSpenderState
{
    [SerializeField] private Transform _markPrefab;

    private HarvesterCreatingState _harvesterCreatingState;
    private ResourcesStorage _storage;
    private HarvestManager _harvestManager;
    private Transform _mark;
    private Vector3 _position;

    public void SpendResources()
    {
        int spawnCost = 5;        

        if (_storage.TrySpendResources(spawnCost))
        {
            if (_harvestManager.TryGetHarvester(out HarvesterWorkerState harvester))
            {
                harvester.BecomeBuilder(_mark.transform.position);
                Destroy(_mark.gameObject);
            }

            _storage.State = _harvesterCreatingState;
        }
    }

    public void SetBuildingPosition(Vector3 point)
    {
        _position = point;
        _storage.State = this;
        SetNewMark();
    }

    private void Awake()
    {
        _harvesterCreatingState = GetComponent<HarvesterCreatingState>();
        _storage = GetComponent<ResourcesStorage>();
        _harvestManager = GetComponent<HarvestManager>();
    }

    private void SetNewMark()
    {
        if (_mark != null)
            Destroy(_mark.gameObject);

        _mark = Instantiate(_markPrefab, _position, Quaternion.identity);
    }
}
