using UnityEngine;

[RequireComponent (typeof(HarvesterWorkerState))]
[RequireComponent (typeof(BuilderWorkerState))]
public class Worker : MonoBehaviour
{ 
    public IWorkerState State { get; set; }

    private void Awake()
    {
        State = GetComponent<HarvesterWorkerState>();
    }

    private void Update()
    {
        State.DoWork();
    }
}
