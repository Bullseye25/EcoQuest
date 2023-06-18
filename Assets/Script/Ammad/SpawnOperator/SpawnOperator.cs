using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOperator : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform spawPoint;

    public void SetPrefab(GameObject newPrefab)
    {
        prefab = newPrefab;
    }

    public void SetSpawningPoint(Transform newSpawnPoint)
    {
        spawPoint = newSpawnPoint;
    }

    public void SpawnPrefab()
    {
        Instantiate(prefab, spawPoint.position, spawPoint.rotation);
    }
}
