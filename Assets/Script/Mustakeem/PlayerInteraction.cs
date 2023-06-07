using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float detectionRange = 5f;
    public float moveSpeed = 1f;
    public Transform targetPosition;
   

    private bool isInRange = false;
    private bool isMoving = false;
    [SerializeField] private GameObject sideImage;
    [SerializeField] private GameObject heartImgae;

    private void Update()
    {
        // Check if the player is within the detection range
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance <= detectionRange)
            {
                // Player is in range
                isInRange = true;
                ShowSide();
            }
            else
            {
                // Player is out of range
                isInRange = false;
                HideSide();
            }
        }

        // Move the object down if F is pressed
        if (isInRange && Input.GetKeyDown(KeyCode.P) && !isMoving)
        {
            isMoving = true;
            StartCoroutine(MoveObjectDown());
        }
    }

    private void ShowSide()
    {
        // TODO: Display the press text
        sideImage.SetActive(true);
        
    }

    private void HideSide()
    {
        // TODO: Hide the press text
         sideImage.SetActive(false);
    }

    private System.Collections.IEnumerator MoveObjectDown()
    {
        while (transform.position.y > targetPosition.position.y)
        {
             sideImage.SetActive(false);
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
            yield return null;
            heartImgae.SetActive(true);
        }

        isMoving = false;
    }
}
