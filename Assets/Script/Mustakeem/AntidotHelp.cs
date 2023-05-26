
using TMPro;
using UnityEngine;

public class AntidotHelp : MonoBehaviour
{
    
   
    [SerializeField] private TextMeshProUGUI scoreText; // Reference to the TextMeshProUGUI component to display the score
    public static int score = 0; // Variable to store the current score

    private void OnCollisionEnter(Collision other)
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
