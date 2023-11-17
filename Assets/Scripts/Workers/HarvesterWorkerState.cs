using UnityEngine;

[RequireComponent(typeof(BuilderWorkerState))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Worker))]

public class HarvesterWorkerState : MonoBehaviour, IWorkerState
{
    [SerializeField] private float _speed;

    private HarvestManager _storage;
    private Transform _target;
    private Animator _animator;
    private bool _isReturning;

    public bool IsBusy { get; private set; }

    public void DoWork()
    {
        if (IsBusy)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.position, Time.deltaTime * _speed);
            transform.LookAt(_target.position);
        }

        _animator.SetBool("IsWalking", IsBusy);
    }

    public void BecomeBuilder(Vector3 newBasePosition)
    {
        if (_target.TryGetComponent(out Resource resource))
            resource.IsMarkedForHarvest = false;

        BuilderWorkerState builder = GetComponent<BuilderWorkerState>();
        GetComponent<Worker>().State = builder;
        builder.SendToNewBaseLocation(newBasePosition);
        enabled = false;
    }

    public void SetStorage(HarvestManager storage)
    {
        _storage = storage;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
        IsBusy = true;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        IsBusy = false;
        _target = null;
        _isReturning = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_target != null && other == _target.GetComponent<Collider>() && _target.TryGetComponent<Resource>(out Resource resouce))
        {
            _target = _storage.transform;
            resouce.transform.SetParent(transform);
            _isReturning = true;
        }
        else if (other == _storage.GetComponent<Collider>() && _isReturning == true)
        {
            IsBusy = false;
            _isReturning = false;
        }
    }
}
