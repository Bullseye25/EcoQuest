using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 3f;
    [SerializeField] private bool rotateWhileMoving = true;
    [SerializeField] private bool allowYRotation;

    [Space]
    [SerializeField] private bool freezeYAxisPos = false;

    private Vector3 previousPosition;

    private void Start()
    {
        // Set the initial previous position to the current position
        previousPosition = transform.position;
    }

    private void Update()
    {
        if (target == null)
            return;

        // Move towards the target position
        if (freezeYAxisPos == false)
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        else 
        {
            var pos = new Vector3(target.position.x, transform.position.y, target.position.z);
            transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);
        }

        if (rotateWhileMoving)
        {
            Vector3 movementDirection = transform.position - previousPosition;

            // Check if the movement direction is from right to left or left to right
            if (movementDirection.x > 0 && movementDirection.z == 0) // Assuming z-axis is the forward/backward axis
            {
                // Rotate the object to 0 degrees in the y-axis
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else if (movementDirection.x < 0 && movementDirection.z == 0)
            {
                // Rotate the object to -180 degrees in the y-axis
                transform.rotation = Quaternion.Euler(0f, -180f, 0f);
            }
            else if (allowYRotation && movementDirection.z != 0)
            {
                // Freely rotate the object in the y-axis based on its movement in the z-axis
                /*   float rotationAngle = Mathf.Atan2(movementDirection.z, movementDirection.x) * Mathf.Rad2Deg;
                   transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);*/

                Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2 * speed * Time.deltaTime);

            }

            previousPosition = transform.position;
        }

        // Update the previous position to the current position for the next frame
        previousPosition = transform.position;
    }

    public void SetTarget(Transform value)
    {
        target = value;
    }

    public void SetFreeRotation(bool value)
    {
        allowYRotation = value;
    }
}
