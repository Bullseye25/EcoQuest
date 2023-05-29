using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
     public GameObject targetObject;

     private Levels level;

    private void Start()
    {
        // Assuming you have assigned the game object in the Inspector
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (targetObject != null)
            {
                MeshCollider meshCollider = targetObject.GetComponent<MeshCollider>();

                if (meshCollider != null)
                {
                    // Your condition here
                    
                        meshCollider.enabled = true;
                        level.NextStage();
                    
                }
            }
        }
    }
}
