using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectiblesSpawner : MonoBehaviour
{
    public GameObject prefab1;
    public GameObject prefab2;
    public Transform[] spawnPoints;

    void Start()
    {
        SpawnCollectibles();
    }

    void SpawnCollectibles()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            
            GameObject collectiblePrefab = Random.Range(0f, 1f) > 0.5f ? prefab1 : prefab2;
            GameObject newCollectible = Instantiate(collectiblePrefab, spawnPoint.position, Quaternion.identity);
            newCollectible.transform.parent = spawnPoint;
            TextMeshPro textMeshPro = newCollectible.GetComponentInChildren<TextMeshPro>();
            
            if (textMeshPro != null)
            {
                int randomValue = Random.Range(1, 20);
                textMeshPro.text = randomValue.ToString();
            }
            else
            {
                Debug.LogError("Component not found.");
            }
        }
    }
}