
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [System.Serializable]


    public struct WaypointData
    {
        public Transform waypoint;
        public float rotationAngle;
        public AnimationClip animation;
    }

    [SerializeField] private WaypointData[] waypoints;
    private int currentWaypointIndex = 0;

    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private GameObject bloodPoint;

    private IMoveBehavior moveBehavior;
    private Animator animator;

    private void Start()
    {
        SetMoveBehavior(new Walk());
        animator = GetComponent<Animator>();
        bloodPoint.SetActive(false);
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].waypoint.position) < 0.1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }

            RotateToWaypointAngle();

            if (waypoints[currentWaypointIndex].animation != null)
            {
                PlayAnimation(waypoints[currentWaypointIndex].animation);
            }
        }

        if (PlayerInRange())
        {
            SetMoveBehavior(new Run()); // Set move behavior to run if player is in range
        }
        else
        {
            SetMoveBehavior(new Walk()); // Set move behavior to walk if player is not in range
        }

        Move();
    }

    private bool PlayerInRange()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && Vector3.Distance(transform.position, player.transform.position) <= detectionRange)
        {
            return true;
        }
        return false;
    }

    private void Move()
    {
        moveBehavior.Move(transform, waypoints[currentWaypointIndex].waypoint, walkSpeed, runSpeed, Time.deltaTime);
    }

    private void SetMoveBehavior(IMoveBehavior newMoveBehavior)
    {
        moveBehavior = newMoveBehavior;
    }

    private void RotateToWaypointAngle()
    {
        float targetAngle = waypoints[currentWaypointIndex].rotationAngle;
        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
    }

    private void PlayAnimation(AnimationClip animation)
    {
        animator.Play(animation.name);
    }
}

public interface IMoveBehavior
{
    void Move(Transform transform, Transform waypoint, float walkSpeed, float runSpeed, float deltaTime);
}

public class Walk : IMoveBehavior
{
    public void Move(Transform transform, Transform waypoint, float walkSpeed, float runSpeed, float deltaTime)
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoint.position, walkSpeed * deltaTime);
    }
}

public class Run : IMoveBehavior
{
    public void Move(Transform transform, Transform waypoint, float walkSpeed, float runSpeed, float deltaTime)
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoint.position, runSpeed * deltaTime);
    }
}
