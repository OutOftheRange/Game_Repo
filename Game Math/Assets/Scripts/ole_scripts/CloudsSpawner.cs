using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] cloudPrefabs;
    private float timer;

    private void Start()
    {
        GenerateCloud();
        timer = Random.Range(1, 5);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            GenerateCloud();
            timer = Random.Range(10, 15);
        }
    }

    private void GenerateCloud()
    {
        Instantiate(cloudPrefabs[Random.Range(0, cloudPrefabs.Length)], new Vector3(Random.Range(13, 17), Random.Range(0, 7), 0), new Quaternion(0, 0, Random.Range(0, 180f), 0), transform);
    }


}
