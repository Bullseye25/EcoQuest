using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class PrefabSpawner : MonoBehaviour
{
    public GameObject prefab;
    public Transform[] positions;
    public TextMeshProUGUI trashCollectedText;

    private int trashCollected;
    private int currentIndex = 0;
    private int currentPrefabIndex = 0;
    private int numPrefabsToShow = 50;
    private List<GameObject> prefabPool = new List<GameObject>();

    private void Start()
    {
        InitializePrefabPool(10); // Create an initial pool of 10 prefabs
        SpawnPrefabsFromPool(currentPrefabIndex, currentPrefabIndex + numPrefabsToShow); // Spawn the first set of prefabs
        currentIndex = numPrefabsToShow;

        TrashPrefab.OnTrashCollected += HandleTrashCollected;
    }

    private void OnDestroy()
    {
        TrashPrefab.OnTrashCollected -= HandleTrashCollected;
    }

    private void InitializePrefabPool(int poolSize)
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newPrefab.SetActive(false);
            prefabPool.Add(newPrefab);
        }
    }

    private void SpawnPrefabsFromPool(int startIndex, int endIndex)
    {
        for (int i = startIndex; i < endIndex; i++)
        {
            GameObject newPrefab = GetPooledPrefab();
            newPrefab.transform.position = positions[i].position;
            newPrefab.SetActive(true);
        }
    }

    private GameObject GetPooledPrefab()
    {
        foreach (GameObject prefab in prefabPool)
        {
            if (!prefab.activeInHierarchy)
            {
                return prefab;
            }
        }

        // If all pooled prefabs are in use, instantiate a new one (expand the pool if necessary)
        GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        newPrefab.SetActive(false);
        prefabPool.Add(newPrefab);
        return newPrefab;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Prefab"))
        {
            TrashPrefab trashPrefab = other.gameObject.GetComponent<TrashPrefab>();
            if (trashPrefab != null)
            {
                trashPrefab.Collect();
                currentIndex++;
            }

            if (currentIndex >= currentPrefabIndex + numPrefabsToShow)
            {
                currentPrefabIndex = currentIndex;
                int endIndex = Mathf.Min(currentPrefabIndex + numPrefabsToShow, positions.Length);

                // If we haven't reached the end yet, spawn the next set of prefabs
                if (currentPrefabIndex < positions.Length)
                {
                    SpawnPrefabsFromPool(currentPrefabIndex, endIndex);
                }
            }

            // If the player hit all prefabs, you can do something here (e.g., display a win message).
            if (currentPrefabIndex >= positions.Length)
            {
                // All prefabs have been hit.
                // You can add the logic for game over or completion here.
                // For now, we'll simply reset the counter to loop through the positions again.
                currentPrefabIndex = 0;
                currentIndex = 0;
                SpawnPrefabsFromPool(currentPrefabIndex, currentPrefabIndex + numPrefabsToShow);
            }
        }
    }

    private void HandleTrashCollected()
    {
        trashCollected++;
        trashCollectedText.text = trashCollected.ToString();
    }
}
