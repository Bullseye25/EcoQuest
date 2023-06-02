using UnityEngine;

public class FishBehaviour : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 3f; // Adjust the movement speed as desired
    [SerializeField] private float yAxisRadius;
    [SerializeField] private float xAxisRadius;

    private Vector3 targetPosition;
    private Quaternion targetRotation;

    private float startingYAxis;
    private float startingXAxis;

    private void Start()
    {
        var position = transform.position;

        startingXAxis = position.x;
        startingYAxis = position.y;

        // Set the initial target position to the starting position

        var x = startingXAxis - xAxisRadius;
        var y = startingYAxis - yAxisRadius;
        targetPosition = new Vector3(x, y, -3f);
        targetRotation = Quaternion.Euler(0f, 0f, 0f);
    }

    private void Update()
    {
        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);

        // Rotate towards the target rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 180f * movementSpeed * Time.deltaTime);

        // If the sea horse reaches the target position, update the new target position and rotation
        if (transform.position == targetPosition)
        {
            RoamAround();
        }
    }

    private void RoamAround()
    {
        float x = 0f;
        float y = Random.Range(startingYAxis - yAxisRadius, startingYAxis + yAxisRadius);

        var currentX = transform.position.x;

        if (currentX >= startingXAxis + xAxisRadius)
        {
            x = startingXAxis - xAxisRadius;
        }
        else if(currentX <= startingXAxis - xAxisRadius)
        {
            x = startingXAxis + xAxisRadius;
        }

        // Generate a random target position within the specified range
        targetPosition = new Vector3(x, y, -3f);

        // Update the target rotation based on the direction of movement
        if (targetPosition.x > transform.position.x)
        {
            targetRotation = Quaternion.Euler(0f, 0, 0f);
        }
        else
        {
            targetRotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }
}