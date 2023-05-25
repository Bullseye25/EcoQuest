using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntidotController : MonoBehaviour
{
    [SerializeField] private     GameObject antidotPrefab;
    [SerializeField] private KeyCode spawnKey = KeyCode.P;
    [SerializeField] private List<GameObject> targetObjects;

    private GameObject currentAntidot;
    private GameObject currentTarget;
    private int antidotCount = 0; // Number of antidots collected
    [SerializeField] private CollectibleSystem collectibleSystem;

    
    
    private void Update()
    {
      //  Debug.Log(collectibleSystem.AntidoteCount);
        antidotCount = collectibleSystem.AntidoteCount;
      //  Debug.Log(antidotCount);
        if (Input.GetKeyDown(spawnKey)  && antidotCount > 0)
        {
           
            SpawnAntidot();
        }

        if (currentAntidot != null && currentTarget != null)
        {
            MoveAntidotTowardsTarget();
        }
    }

    public void SpawnAntidot()
    {
        if(antidotCount > 0)
        {
            if (currentAntidot != null)
            {
                Destroy(currentAntidot);
            }

            // Instantiate antidot prefab at player's position
            currentAntidot = Instantiate(antidotPrefab, transform.position, Quaternion.identity);

            // Find the nearest target object
            currentTarget = GetNearestObject();
            
            // if (currentTarget != null)
            // {
            //     Debug.Log("Moving antidot towards target.");
            // }
            // else
            // {
            //     Debug.LogWarning("No target object found.");
            // }
        }
        
    }

    private GameObject GetNearestObject()
    {
        GameObject nearestObject = null;
        float minDistance = 5f; // Initialize to a large value

        foreach (GameObject target in targetObjects)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                nearestObject = target;
            }
        }

        return nearestObject;
    }

    private void MoveAntidotTowardsTarget()
    {
        Vector3 direction = (currentTarget.transform.position - currentAntidot.transform.position).normalized;
        currentAntidot.GetComponent<Rigidbody>().velocity = direction * 5f; // Adjust the speed as needed
    }

    
}
