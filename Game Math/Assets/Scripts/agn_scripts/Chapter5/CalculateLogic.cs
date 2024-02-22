using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CalculateLogic : MonoBehaviour
{
    public TextMeshProUGUI displayText;
    public TextMeshProUGUI displayText2;
    public TextMeshProUGUI displayText3;
    private List<string> collectedChestValues = new();

    void Start()
    {
        StartCoroutine(ListCollectiblesDelay(0.1f));
    }

    IEnumerator ListCollectiblesDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ListCollectibles();
    }

    //takes all chest values
    private void ListCollectibles()
    {
        GameObject[] collectiblesObjects = GameObject.FindGameObjectsWithTag("Chest");

        if (collectiblesObjects == null || collectiblesObjects.Length == 0)
        {
            Debug.LogError("Chests not found");
            return;
        }

        foreach (GameObject collectiblesObject in collectiblesObjects)
        {
            TextMeshPro[] textMeshPros = collectiblesObject.GetComponentsInChildren<TextMeshPro>();

            foreach (TextMeshPro textMeshPro in textMeshPros)
            {
                string tekst = textMeshPro.text;
                collectedChestValues.Add(tekst);
            }
        }

        //Debug.Log("Collected values " + string.Join(", ", collectedChestValues));
        RandomValue(collectedChestValues);
    }

    //picks value and calculates
    private void RandomValue(List<string> collectedChestValues)
    {
        if (collectedChestValues.Count > 0)
        {
            int randomIndex = Random.Range(0, collectedChestValues.Count);
            string randomValueString = collectedChestValues[randomIndex];

            if (int.TryParse(randomValueString, out int randomValue))
            {
                //Debug.Log("Chosen value: " + randomValue);
                int randomSecValue = Random.Range(2, 4);
                int newValue = (int)Mathf.Pow(randomValue, randomSecValue);
                displayText.text = newValue.ToString();
                displayText2.text = "Jaka liczba zosta³a podniesiona do potêgi" ;
                displayText3.text = randomSecValue.ToString();
            }
            else
            {
                Debug.LogError("Conversion failed");
            }
        }
        else
        {
            Debug.LogError("No values");
        }
    }  
}