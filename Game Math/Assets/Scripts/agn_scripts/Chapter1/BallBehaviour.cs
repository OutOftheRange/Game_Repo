using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject[] wayPoints;
    private int currentWayPoint = 0;
    [SerializeField] private float speed = 50f;

    private void Update()
    {
        UpdateWaypoint();
        MoveTowardsWaypoint();
    }

    private void UpdateWaypoint()
    {
        if (Vector2.Distance(wayPoints[currentWayPoint].transform.position, transform.position) < .1f)
        {
            currentWayPoint = (currentWayPoint + 1) % wayPoints.Length;
        }
    }

    private void MoveTowardsWaypoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, wayPoints[currentWayPoint].transform.position, Time.deltaTime * speed);
    }
}
