using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CheckResult : MonoBehaviour
{
    public string playerTag = "Player";
    public string movableObjectTag = "MovableObject";
    public string childObjectTag = "ChildObject";
    public string numberToFind1Tag = "NumberToFind1";
    public string numberToFind2Tag = "NumberToFind2";

    private JsonChangeData modifyGameData;

    void Start()
    {
        modifyGameData = JsonChangeData.Instance;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        { 
            CheckTouchingObjects();
        }
    }

    private void CheckTouchingObjects()
    {
        int valueTextMeshPro1 = GetTextMeshProValueWithTag(numberToFind1Tag);
        int valueTextMeshPro2 = GetTextMeshProValueWithTag(numberToFind2Tag);

        GameObject[] movableObjects = GameObject.FindGameObjectsWithTag(movableObjectTag);

        bool movableObject1Found = false;
        bool movableObject2Found = false;

        foreach (GameObject movableObject in movableObjects)
        {
            GameObject[] childObjects = GameObject.FindGameObjectsWithTag(childObjectTag);

            foreach (GameObject childObject in childObjects)
            {
                Collider2D movableObjectCollider = movableObject.GetComponent<Collider2D>();
                Collider2D childObjectCollider = childObject.GetComponent<Collider2D>();

                if (movableObjectCollider.OverlapPoint(childObjectCollider.bounds.center))
                {
                    int valueChildObject = childObject.GetComponent<ObjectsValue>().TakeItemValue();
                    Debug.Log("ChildObject: " + valueChildObject);

                    if (valueChildObject == valueTextMeshPro1)
                    {
                        movableObject1Found = true;
                    }

                    if (valueChildObject == valueTextMeshPro2)
                    {
                        movableObject2Found = true;
                    }
                }
            }
        }

        if (movableObject1Found && movableObject2Found)
        {
            //Debug.Log("Player guessed both values");
            EndlessModeManager();
        }
        else
        {
            RestartLevel();
        }
    }
    
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void EndlessModeManager()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == "Scene4a_agn")
        {
            SceneManager.LoadScene("Scene4b_agn");
        }
        else if (currentSceneName == "Scene4b_agn")
        {
            SceneManager.LoadScene("Scene4c_agn");
        }
        else if (currentSceneName == "Scene4c_agn" || currentSceneName == "Scene4_agn")
        {
            SceneManager.LoadScene("EndScene_agn");
            if (SceneButtonsLogic.agn_plot_scene_variable == 1)
            {
                modifyGameData.ModifyGameProgress("Chapter5_Level1", true);
            }
        }
    }

    private int GetTextMeshProValueWithTag(string tag)
    {
        GameObject textMeshProObject = GameObject.FindGameObjectWithTag(tag);

        if (textMeshProObject != null)
        {
            TextMeshProUGUI textMeshPro = textMeshProObject.GetComponent<TextMeshProUGUI>();

            if (textMeshPro != null)
            {
                int value;
                if (int.TryParse(textMeshPro.text, out value))
                {
                    return value;
                }
            }
        }
        return 0;
    }
}
