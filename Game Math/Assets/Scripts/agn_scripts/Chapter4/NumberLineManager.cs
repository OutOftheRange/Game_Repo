using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class NumberLineManager : MonoBehaviour
{
    private int[] actualArray;
    private int[] firstLevel = new int[] { -4, -3, -2, -1, 0, 1, 2, 3, 4 };
    private int[] secondLevel = new int[] { -8, -6, -4, -2, 0, 2, 4, 6, 8 };
    private int[] thirdLevel = new int[] { -40, -30, -20, -10, 0, 10, 20, 30, 40 };
    private int[] fourthLevel = new int[] { -20, -15, -10, -5, 0, 5, 10, 15, 20 };
    private int[][] allArrays;

    public TextMeshProUGUI textMeshPro1; 
    public TextMeshProUGUI textMeshPro2;
    public TextMeshProUGUI textMeshPro3; 
    public TextMeshProUGUI textMeshPro4; 

    private void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        switch (sceneName)
        {
            case "Scene4a_agn":
                actualArray = firstLevel;
                break;
            case "Scene4b_agn":
                actualArray = secondLevel;
                break;
            case "Scene4c_agn":
                actualArray = thirdLevel;
                break;
            default:
                allArrays = new int[][] { firstLevel, secondLevel, thirdLevel, fourthLevel };
                int randomArray = Random.Range(0, allArrays.Length);
                actualArray = allArrays[randomArray];
                break;
        }

        SetLineNumbers();
        AddValuesToChildObjects();
        TwoRandomArrayValues();
    }

    private void AddValuesToChildObjects()
    {
        GameObject parentObject = GameObject.FindWithTag("ParentObject");

        if (parentObject != null)
        {
            Transform parentTransform = parentObject.transform;

            int numberOfChildren = parentTransform.childCount;

            for (int i = 0; i < Mathf.Min(numberOfChildren, actualArray.Length); i++)
            {
                Transform child = parentTransform.GetChild(i);
                int valueFromArray = actualArray[i];
                child.GetComponent<ObjectsValue>().SetItemValue(valueFromArray);
            }
        }
        else
        {
            Debug.LogError("Parent not found");
        }
    }

    //check child object value
    /* 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ChildObject"))
        {
            int childValue = other.GetComponent<ObjectsValue>().TakeItemValue();
            Debug.Log("Child value: " + childValue);
        }
    }
    */

    private void SetLineNumbers()
    {
        {
            if (actualArray.Length >= 8)
            {
                int valueFromArrayPosition3 = actualArray[3]; //first line value to show
                int valueFromArrayPosition5 = actualArray[5]; //second line value to show

                if (textMeshPro1 != null)
                {
                    textMeshPro1.text = valueFromArrayPosition3.ToString();
                }

                if (textMeshPro2 != null)
                {
                    textMeshPro2.text = valueFromArrayPosition5.ToString();
                }
            }
            else
            {
                Debug.LogWarning("Array too short");
            }
        }

    }

    private void TwoRandomArrayValues()
    {
        if (actualArray.Length >= 2)
        {
            int index1 = Random.Range(0, actualArray.Length - 1); //random first value to find
            int index2 = Random.Range(index1 + 1, actualArray.Length); //random second value to find

            int valueToFind1 = actualArray[index1];
            int valueToFind2 = actualArray[index2];

            Debug.Log("Values to find: " + valueToFind1 + ", " + valueToFind2);

            if (textMeshPro3 != null)
            {
                textMeshPro3.text = valueToFind1.ToString();
            }

            if (textMeshPro4 != null)
            {
                textMeshPro4.text = valueToFind2.ToString();
            }
        }
        else
        {
            Debug.LogWarning("Array too short");
        }
    }
}
