using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SharkBehaviour : MonoBehaviour
{
    private const float Z_AXIS = -3;

    [SerializeField] private float movementSpeed = 3f; // Adjust the movement speed as desired
    [SerializeField] private float radius;
    
    private float xAxisPosition;
    private float yAxisPosition;

    private Vector3 rotation;
    private float yAxisRotation;

    private bool movingRight = true;
    private Vector3 targetPosition;

    private void Start()
    {
        var position = transform.position;

        xAxisPosition = position.x;
        yAxisPosition = position.y;

        // Set the initial target position to the starting x-axis position
        targetPosition = new Vector3(position.x, yAxisPosition, Z_AXIS);
        rotation = transform.rotation.eulerAngles;
        yAxisRotation = rotation.y;
    }

    private void Update()
    {
        var currentPosition = transform.position;

        // Move towards the target position
        transform.position = Vector3.MoveTowards(currentPosition, targetPosition, movementSpeed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(rotation.x, yAxisRotation, rotation.z), 180f * movementSpeed * Time.deltaTime);

        // If the shark reaches the target position, update the new target position
        if (currentPosition == targetPosition)
        {
            UpdateTargetPosition();
        }
    }

    private void UpdateTargetPosition()
    {
        // If the shark is moving right, update the target position towards the maximum x-axis position
        // Otherwise, update the target position towards the minimum x-axis position
        if (movingRight)
        {
            targetPosition = new Vector3(xAxisPosition + radius, yAxisPosition, Z_AXIS);
            yAxisRotation += 180;
        }
        else
        {
            targetPosition = new Vector3(xAxisPosition + (-radius), yAxisPosition, Z_AXIS);
            yAxisRotation -= 180;
        }

        // Toggle the moving direction
        movingRight = !movingRight;
    }

    public void AttackMode()
    {
        movementSpeed = 12.5f;

        IEnumerator CalmDown()
        {
            yield return new WaitForSeconds(3f);
            movementSpeed = 3;
        }

        StartCoroutine(CalmDown());
    }
}
