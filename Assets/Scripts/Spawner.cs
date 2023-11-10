using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _creationPrefab;
    [SerializeField] private Terrain _terrain;

    private Vector2 _minXZ;
    private Vector2 _maxXZ;

    public GameObject[] Spawn(int amount)
    {
        GameObject[] creations = new GameObject[amount];
        float minX = _terrain.GetPosition().x;
        float minZ = _terrain.GetPosition().z;
        float maxX = _terrain.terrainData.size.x + minX;
        float maxZ = _terrain.terrainData.size.z + minZ;
        _minXZ = new Vector2(minX, minZ);
        _maxXZ = new Vector2(maxX, maxZ);

        for (int i = 0; i < amount; i++)
            creations[i] = Instantiate(_creationPrefab, GetRandomPointOnTerrain(_minXZ, _maxXZ), Quaternion.identity, _terrain.transform);

        return creations;
    }

    private Vector3 GetRandomPointOnTerrain(Vector2 minXZ, Vector2 maxXZ)
    {
        float resourceHeight = 1;
        Vector3 point = new Vector3(Random.Range(minXZ.x, maxXZ.x),
                                    0,
                                    Random.Range(minXZ.y, maxXZ.y));

        point.y = _terrain.SampleHeight(point) + _terrain.GetPosition().y + resourceHeight;

        return point;
    }
}
