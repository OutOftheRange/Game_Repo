using UnityEngine;

public class AnimatedObjectMovement : MonoBehaviour
{
    public GameObject[] waypoints;
    private int currentWaypointIndex = 0;
    public float objectSpeed = 5f;

    void Update()
    {
        if (waypoints == null || waypoints.Length < 2)
        {
            Debug.LogError("Missing waypoints");
            return;
        }

        float step = objectSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, step);

        if (Vector2.Distance(transform.position, waypoints[currentWaypointIndex].transform.position) < 0.1f)
        {
            ChangeWaypoint();
            RotateObject();
        }
    }

    void ChangeWaypoint()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }

    void RotateObject()
    {
        transform.Rotate(0f, 180f, 0f);
    }
}
