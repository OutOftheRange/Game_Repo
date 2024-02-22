using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectiblesSpawner2 : MonoBehaviour
{
    public GameObject prefab1;
    public GameObject prefab2;
    public GameObject prefab3;
    public Transform[] spawnPoints1;
    public Transform[] spawnPoints2;

    void Start()
    {
        SpawnCollectibles(prefab1, prefab2, spawnPoints1);
        SpawnCollectibles(prefab3, null, spawnPoints2);
    }

    void SpawnCollectibles(GameObject collectiblePrefab1, GameObject collectiblePrefab2, Transform[] spawnPoints)
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            GameObject collectiblePrefab = (collectiblePrefab2 != null && Random.Range(0f, 1f) > 0.5f) ? collectiblePrefab2 : collectiblePrefab1;
            GameObject newCollectible = Instantiate(collectiblePrefab, spawnPoint.position, Quaternion.identity);
            newCollectible.transform.parent = spawnPoint;
            TextMeshPro textMeshPro = newCollectible.GetComponentInChildren<TextMeshPro>();

            if (textMeshPro != null)
            {
                int randomValue = Random.Range(1, 6);
                textMeshPro.text = randomValue.ToString();
            }
            else
            {
                Debug.LogError("TextMeshPro not found");
            }
        }
    }
}
