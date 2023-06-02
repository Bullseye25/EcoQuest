
using TMPro;
using UnityEngine;

public class AntidotHelp : MonoBehaviour
{
    
   
    public static int score = 0;

    public void OnCollisionEnter(Collision other)
{
    if (other.gameObject.CompareTag("Target"))
    {
        Destroy(gameObject);
        
        EnemyController movementScript = other.gameObject.GetComponent<EnemyController>();
        if (movementScript != null)
        {
            movementScript.enabled = true;
        }
        
        // Add score and update the text
       score++;
        
        
    }
   
}

//    private void UpdateScoreText()
//     {
    
//         Debug.Log("qwre");
           
        
//     }
}
