using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntidotHelp : MonoBehaviour
{
    // Start is called before the first frame update
   
     private void OnCollisionEnter(Collision other)
    
    {
     //   Debug.Log(other.gameObject);
        if (other.gameObject.CompareTag("Target") )
        {
            // Destroy the antidot prefab
            Destroy(gameObject);

            // Enable the Movement script on the collided target object
            EnemyController movementScript = other.gameObject.GetComponent<EnemyController>();
            if (movementScript != null)
            {
                movementScript.enabled = true;
            }
        }
    }
}
