using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NodesPath : MonoBehaviour
{
    [SerializeField] private float speed;
    
    [Space]
    [SerializeField] private bool doLoop;
    [SerializeField] private Transform[] points;
    
    [Space]
    [SerializeField] private UnityEvent uponComplete = new UnityEvent();

    private int currentPointIndex = 0;

    protected virtual void Update()
    {
        // Check if there are points to move towards
        if (points.Length > 0)
        {
            // Get the current point to move towards
            Transform currentPoint = points[currentPointIndex];

            // Move towards the current point
            transform.position = Vector3.MoveTowards(transform.position, currentPoint.position, speed * Time.deltaTime);

            // Look towards the current point on the y-axis
            Vector3 lookDirection = currentPoint.position - transform.position;
            // lookDirection.y = 0f; // Lock rotation on the y-axis
            if (lookDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2 * speed * Time.deltaTime);
            }

            // Check if the game object has reached the current point
            if (transform.position == currentPoint.position)
            {
                // Increment the current point index
                currentPointIndex++;

                // Check if we have reached the end of the points array
                if (currentPointIndex >= points.Length)
                {
                    if (doLoop)
                    {
                        // Wrap around to the start if doLoop is true
                        currentPointIndex = 0;
                    }
                    else
                    {
                        // Invoke the "uponComplete" UnityEvent if doLoop is false
                        uponComplete.Invoke();

                        // Stop the movement or perform any other actions
                        // if desired when reaching the end point
                        return;
                    }
                }
            }
        }
    }
}
