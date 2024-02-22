using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemCollector2 : MonoBehaviour
{
    public TextMeshProUGUI endScore;
    float number = 0;
    GameObject currentCollectible = null;
    private static float winValue = 0;
    private bool isWinValueCalculated = false;
    public float delayTime = 1.0f; 
    public float disappearingTime = 0.5f;
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
            winValue = CalculateWinValue(GetRandomStartValue());
            isWinValueCalculated = true;
            endScore.text = "Wartoœæ do znalezienia: " + winValue.ToString();
        }
    }

    //choosing start value
    private float GetRandomStartValue()
    {
        GameObject[] startValueObjects = GameObject.FindGameObjectsWithTag("StartValue");
        List<float> startValues = new();

        foreach (GameObject startValueObject in startValueObjects)
        {
            TextMeshPro textMeshPro = startValueObject.GetComponentInChildren<TextMeshPro>();

            if (textMeshPro != null)
            {
                if (float.TryParse(textMeshPro.text, out float value))
                {
                    startValues.Add(value);
                }
                else
                {
                    Debug.LogError("Conversion failed");
                }
            }
            else
            {
                Debug.LogWarning("TextMeshPro not found");
            }
        }

        if (startValues.Count > 0)
        {
            int randomIndex = Random.Range(0, startValues.Count);
            return startValues[randomIndex];
        }
        return 0; 
    }

    //calculating win value using lists to choose objects with values
    private float CalculateWinValue(float selectedUniqueValue)
    {
        GameObject[] collectiblesObjects = GameObject.FindGameObjectsWithTag("Spawning");

        if (collectiblesObjects == null || collectiblesObjects.Length == 0)
        {
            Debug.LogError("Spawning objects not found");
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
            Debug.LogError("Spawning not found");
            return 0;
        }

        List<GameObject> selectedCollectibles = new();

        if (allCollectibles.Count > 0)
        {
            float numberOfObjectsToSelect = 2;

            for (float i = 0; i < numberOfObjectsToSelect; i++)
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

        Debug.Log("Unique value " + selectedUniqueValue);

        float result = selectedUniqueValue;

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
                        if (tmp.transform.parent.CompareTag("Collectible3"))
                        {
                            result *= selectedValue;
                            valueAdded = true;
                        }
                        else if (tmp.transform.parent.CompareTag("Collectible4"))
                        {
                            if (selectedValue != 0)
                            {
                                result /= selectedValue;
                                valueAdded = true;
                            }
                            else
                            {
                                Debug.LogError("Can't divide by zero.");
                            }
                        }
                        break;
                    }
                }
                if (valueAdded)
                {
                    break;
                }
            }
            Debug.Log("Win value " + result);       
        }
        return (float)result;
    }

    void DestroyObjectByTag(string tag)
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);

        foreach (GameObject obj in objectsWithTag)
        {
            StartCoroutine(ChangeVisibility(obj));
        }
    }

    //objects disappearing animation
    IEnumerator ChangeVisibility(GameObject obj)
    {
        float disappearingTime = 0.5f; 
        float startTime = Time.time;
        Renderer renderer = obj.GetComponent<Renderer>();

        if (renderer == null)
        {
            yield break; 
        }

        Color startColor = renderer.material.color;

        while (Time.time - startTime < disappearingTime)
        {
            if (renderer == null)
            {
                yield break; 
            }

            float ongoingTime = Time.time - startTime;
            float disappearingPercent = ongoingTime / disappearingTime;
            Color nowyKolor = new Color(startColor.r, startColor.g, startColor.b, 1 - disappearingPercent);
            renderer.material.color = nowyKolor;
            yield return null;
        }
        Destroy(obj);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("StartValue") || other.CompareTag("Collectible3") || other.CompareTag("Collectible4"))
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

            if (float.TryParse(text, out float result))
            {
                switch (currentCollectible.tag)
                {
                    case "StartValue":
                        number += result;
                        Debug.Log("Collected " + number);
                        DestroyObjectByTag("Wall");
                        DestroyObjectByTag("StartValue");
                        break;
                    case "Collectible3":
                        number *= result;
                        Debug.Log("Collected " + number);
                        break;
                    case "Collectible4":
                        if (result != 0)
                        {
                            number /= result;
                            Debug.Log("Collected " + number);
                        }
                        else
                        {
                            Debug.LogError("Can't divide by zero");
                        }
                        break;
                    default:
                        Debug.LogWarning("Unknown tag");
                        break;
                }

                if (number == winValue)
                {
                    //Debug.Log("Win");
                    if (SceneButtonsLogic.agn_plot_scene_variable == 1)
                    {
                        modifyGameData.ModifyGameProgress("Chapter3_Level1", true);
                    }
                    SceneManager.LoadScene("EndScene_agn");
                }

                Destroy(currentCollectible);
            }
            else
            {
                Debug.LogError("Conversion failed");
            }
        }
    }
}