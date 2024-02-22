using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Transform player;
    private Vector3 playerPosition;
    private Vector3 obstaclePosition;
    private float offsetY = -0.5f;

    void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void SetUpLine(Transform obstacle)
    {
        lineRenderer.positionCount = 2;
        playerPosition += player.position + new Vector3(0, offsetY, 1);
        obstaclePosition += obstacle.position + new Vector3(0, offsetY, 1);
        lineRenderer.SetPosition(0, playerPosition);
        lineRenderer.SetPosition(1, obstaclePosition);
    }

}
