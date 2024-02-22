using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CollectChest : MonoBehaviour
{
    GameObject currentCollectible = null;
    private Animator monsterAnimator;

    private JsonChangeData modifyGameData;

    void Start()
    {
        modifyGameData = JsonChangeData.Instance;
        MonsterDeath();
    }

    void MonsterDeath()
    {
        GameObject monsterObject = GameObject.FindGameObjectWithTag("Monster");

        if (monsterObject != null)
        {
            monsterAnimator = monsterObject.GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("Monster object not found!");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Chest"))
        {
            currentCollectible = other.gameObject;
        }
    }

    void Update()
    {
        //choosing answer by player logic
        if (Input.GetKeyDown(KeyCode.F) && currentCollectible != null && currentCollectible.GetComponent<Collider2D>().IsTouching(GetComponent<Collider2D>()))
        {
            TextMeshPro textMeshPro = currentCollectible.GetComponentInChildren<TextMeshPro>();

            if (textMeshPro != null)
            {
                string takenText = textMeshPro.text;
                //Debug.Log("Taken text: " + takenText);

                if (int.TryParse(takenText, out int playerValue))
                {
                    Destroy(currentCollectible);

                    int checkedValue = CheckWinChestValue();
                    if (playerValue == checkedValue)
                    {
                        //Debug.Log("Win");
                        if (monsterAnimator != null)
                        {
                            monsterAnimator.SetBool("IsWin", true);
                        }
                        else
                        {
                            Debug.LogError("Monster Animator not found!");
                        }

                        if (SceneButtonsLogic.agn_plot_scene_variable == 1)
                        {
                            modifyGameData.ModifyGameProgress("Chapter6_Level1", true);
                        }
                        StartCoroutine(ShowEndScene());
                    }
                    else
                    {
                        //Debug.Log("Loss");
                        RestartLevel();
                    }
                }
                else
                {
                    Debug.LogError("Convserion failed");
                }
            }
            else
            {
                Debug.LogError("TextMeshPro not found.");
            }
        }
    }

    //win loss login
    private int CheckWinChestValue()
    {
        int winChestValue = 0;

        GameObject objectToCheck1 = GameObject.FindGameObjectWithTag("ValueToCheck");
        GameObject objectToCheck2 = GameObject.FindGameObjectWithTag("ValueToCheck2");

        if (objectToCheck1 != null && objectToCheck2 != null)
        {
            TextMeshProUGUI textMeshPro1 = objectToCheck1.GetComponentInChildren<TextMeshProUGUI>();
            TextMeshProUGUI textMeshPro2 = objectToCheck2.GetComponentInChildren<TextMeshProUGUI>();

            if (textMeshPro1 != null && textMeshPro2 != null)
            {
                string textValue1 = textMeshPro1.text;
                string textValue2 = textMeshPro2.text;

                if (int.TryParse(textValue1, out int convertedTextValue1) && int.TryParse(textValue2, out int convertedTextValue2))
                {
                    winChestValue = Mathf.RoundToInt(Mathf.Pow(convertedTextValue1, 1f / convertedTextValue2));
                    //Debug.Log("Win value: " + winChestValue);
                }
                else
                {
                    Debug.LogError("Conversion failed");
                }
            }
            else
            {
                Debug.LogError("TextMeshPro not found");
            }
        }
        else
        {
            Debug.LogError("ValueToCheck and ValueToCheck2 not found");
        }
        return winChestValue;
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator ShowEndScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("EndScene_agn");
    }
}
