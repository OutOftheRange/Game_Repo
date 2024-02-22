using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemCollector : MonoBehaviour
{
    public TextMeshProUGUI endScore;
    int number = 0;
    GameObject currentCollectible = null;
    private static float winValue = 0;
    private bool isWinValueCalculated = false;
    private JsonChangeData modifyGameData;

    void Start()
    {
        modifyGameData = JsonChangeData.Instance;
        Invoke("CalculateWithDelay", 0.2f);
    }

    void CalculateWithDelay()
    {
        if (!isWinValueCalculated)
        {
            winValue = CalculateWinValue();
            isWinValueCalculated = true;
            endScore.text = "Wartość do znalezienia: " + winValue.ToString();
        }
    }

    //calculating win value using lists to choose objects with values
    private int CalculateWinValue()
    {
        GameObject[] collectiblesObjects = GameObject.FindGameObjectsWithTag("Spawning");

        if (collectiblesObjects == null || collectiblesObjects.Length == 0)
        {
            Debug.LogError("'Spawning' not found.");
            return 0;
        }

        List<GameObject> allCollectibles = new();

        foreach (GameObject collectiblesObject in collectiblesObjects)
        {
            TextMeshPro[] textMeshPros = collectiblesObject.GetComponentsInChildren<TextMeshPro>();

            if (textMeshPros.Length > 0)
            {
                allCollectibles.Add(collectiblesObject);
            }
        }

        if (allCollectibles.Count == 0)
        {
            Debug.LogError("'Spawning' not found.");
            return 0;
        }

        List<GameObject> selectedCollectibles = new();

        if (allCollectibles.Count > 0)
        {
            int numberOfObjectsToSelect = Random.Range(2, 4);

            if (allCollectibles.Count < numberOfObjectsToSelect)
            {
                numberOfObjectsToSelect = allCollectibles.Count;
            }

            for (int i = 0; i < numberOfObjectsToSelect; i++)
            {
                int randomIndex = Random.Range(0, allCollectibles.Count);

                if (!selectedCollectibles.Contains(allCollectibles[randomIndex]))
                {
                    selectedCollectibles.Add(allCollectibles[randomIndex]);
                }
                else
                {
                    i--;
                }
            }
        }
        else
        {
            Debug.LogError("Spawning not found");
            return 0;
        }

        List<float> selectedValues = new();

        foreach (GameObject selectedCollectible in selectedCollectibles)
        {
            TextMeshPro[] textMeshPros = selectedCollectible.GetComponentsInChildren<TextMeshPro>();

            foreach (TextMeshPro tmp in textMeshPros)
            {
                if (float.TryParse(tmp.text, out float value) && !selectedValues.Contains(value))
                {
                    selectedValues.Add(value);
                }
            }
        }

        if (selectedValues.Count == 0)
        {
            return 0;
        }

        Debug.Log("Unique values " + string.Join(", ", selectedValues));

        float result = 0;

        foreach (float selectedValue in selectedValues)
        {
            bool valueAdded = false;

            foreach (GameObject selectedCollectible in selectedCollectibles)
            {
                TextMeshPro[] textMeshPros = selectedCollectible.GetComponentsInChildren<TextMeshPro>();

                foreach (TextMeshPro tmp in textMeshPros)
                {
                    if (float.TryParse(tmp.text, out float value) && value == selectedValue)
                    {
                        if (tmp.transform.parent.CompareTag("Collectible1"))
                        {
                            result += selectedValue;
                            valueAdded = true;
                        }
                        else if (tmp.transform.parent.CompareTag("Collectible2"))
                        {
                            result -= selectedValue;
                            valueAdded = true;
                        }
                        break;
                    }
                }

                if (valueAdded)
                {
                    break;
                }
            }
        }
        Debug.Log("Win value " + result);
        return (int)result;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collectible1") || other.CompareTag("Collectible2"))
        {
            currentCollectible = other.gameObject;
        }
    }

    void Update()
    {
        //collecting collectibles by player logic
        if (Input.GetKeyDown(KeyCode.F) && currentCollectible != null && currentCollectible.GetComponent<Collider2D>().IsTouching(GetComponent<Collider2D>()))
        {
            string text = currentCollectible.GetComponentInChildren<TextMeshPro>().text;

            if (int.TryParse(text, out int result))
            {
                switch (currentCollectible.tag)
                {
                    case "Collectible1":
                        number += result;
                        Debug.Log("Collected " + number);
                        break;
                    case "Collectible2":
                        number -= result;
                        Debug.Log("Collected " + number);
                        break;
                    default:
                        Debug.LogWarning("Unknown collectible tag");
                        break;
                }

                if (number == winValue)
                {
                    //Debug.Log("Win");
                    if (SceneButtonsLogic.agn_plot_scene_variable == 1)
                    {
                        modifyGameData.ModifyGameProgress("Chapter2_Level1", true);
                    }
                    SceneManager.LoadScene("EndScene_agn");
                }

                Destroy(currentCollectible);
            }
            else
            {
                Debug.LogError("Int convertion failed");
            }
        }
    }
}