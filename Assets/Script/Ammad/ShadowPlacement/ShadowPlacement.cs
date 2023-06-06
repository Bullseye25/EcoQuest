using System.Collections;
using UnityEngine;

public class ShadowPlacement : MonoBehaviour
{
    public LayerMask groundLayerMask; // The layer mask to ignore certain layers
    public Transform target; // The transform to move to the detected ground position
    public Transform raycastStartPoint; // The transform representing the starting point of the raycast

    private int limit = 3;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);

        for (int index = 0; index < limit; index++)
        {
            PerformRaycast();
        }
    }

    private void PerformRaycast()
    {
        Ray ray = new Ray(raycastStartPoint.position, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 15))
        {
            // Move the target to the detected ground position's y-axis
            Vector3 newPosition = target.position;
            newPosition.y = hit.point.y;
            target.position = newPosition;
        }
    }
}