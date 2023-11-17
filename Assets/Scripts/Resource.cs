using UnityEngine;

public class Resource : MonoBehaviour
{
    public bool IsMarkedForHarvest; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ResourcesStorage>(out ResourcesStorage storage))
        { 
            Destroy(transform.gameObject);
            storage.AddResource();
        }
    }
}
