using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChestSpawner : MonoBehaviour
{
    public GameObject objectPrefab;
    public Transform[] spawnPoints; 

    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        int numberOfObjectsToSpawn = Random.Range(2, 11);
        List<Transform> usedSpawnPoints = new List<Transform>();

        for (int i = 0; i < numberOfObjectsToSpawn; i++)
        {
            Transform spawnPoint = GetUnusedSpawnPoint(usedSpawnPoints);

            if (spawnPoint == null)
            {
                Debug.LogWarning("All spawn points taken");
                break;
            }

            GameObject spawnedObject = Instantiate(objectPrefab, spawnPoint.position, Quaternion.identity);
            TextMeshPro textMesh = spawnedObject.GetComponentInChildren<TextMeshPro>();

            if (textMesh != null)
            {
                textMesh.text = (i + 2).ToString(); 
            }
            else
            {
                Debug.LogError("TextMeshProUGUI not found");
            }
            usedSpawnPoints.Add(spawnPoint);
        }
    }

    Transform GetUnusedSpawnPoint(List<Transform> usedSpawnPoints)
    {
        List<Transform> availableSpawnPoints = new List<Transform>();

        foreach (Transform spawnPoint in spawnPoints)
        {
            if (!usedSpawnPoints.Contains(spawnPoint))
            {
                availableSpawnPoints.Add(spawnPoint);
            }
        }

        if (availableSpawnPoints.Count > 0)
        {
            int randomIndex = Random.Range(0, availableSpawnPoints.Count);
            return availableSpawnPoints[randomIndex];
        }
        return null;
    }
}