using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float leftBound = -17;

    private void Start()
    {
        speed = Random.Range(speed - 0.3f, speed + 0.1f);
    }

    private void Update()
    {
        transform.Translate(-Time.deltaTime * speed, 0, 0, Space.World);

        if (transform.position.x <= leftBound)
        {
            Destroy(gameObject);
        }
    }
}
