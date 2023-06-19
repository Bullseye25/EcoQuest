using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOperator : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform spawPoint;
    [SerializeField] private bool enable;

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
        if (enable == false)
            Instantiate(prefab, spawPoint.position, spawPoint.rotation);
        else
        {
            prefab.SetActive(true);
            prefab.transform.localPosition = spawPoint.localPosition;
            prefab.transform.localRotation = spawPoint.localRotation;
        }
    }
}
