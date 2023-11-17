using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BuilderWorkerState : MonoBehaviour, IWorkerState
{
    [SerializeField] private float _speed;
    [SerializeField] private HarvestManager _basePrefab;

    private Vector3 _target;
    private Animator _animator;

    public void DoWork()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, Time.deltaTime * _speed);
        transform.LookAt(_target);
        _animator.SetBool("IsWalking", true);

        if (transform.position == _target)        
            BuildNewBase();        
    }

    public void SendToNewBaseLocation(Vector3 target)
    {
        _target = target;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void BuildNewBase()
    {
        HarvestManager newBase = Instantiate(_basePrefab, transform.position, Quaternion.identity);
        var harvester = GetComponent<HarvesterWorkerState>();
        newBase.AddHarvester(harvester);
        harvester.SetStorage(newBase);
        harvester.enabled = true;
        GetComponent<Worker>().State = harvester;
    }
}
