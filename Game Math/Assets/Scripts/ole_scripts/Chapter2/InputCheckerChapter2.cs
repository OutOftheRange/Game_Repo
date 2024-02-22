using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class InputCheckerChapter2 : MonoBehaviour
{
    [SerializeField] private GameObject controllerObject;
    [SerializeField] private GameManager gameManager;

    private ControllerChapter2 controller;
    private Image imageComponent;
    private Color redColor;

    private string lastAttempt;

    //JSON logic
    private JsonChangeData modifyGameData;

    private void Awake()
    {
        controller = controllerObject.GetComponent<ControllerChapter2>();
        imageComponent = gameObject.GetComponent<Image>();
    }

    private void Start()
    {
        redColor = new Color(0.95f, 0.2f, 0.2f, 0.86f);
        lastAttempt = "";
        //JSON logic
        modifyGameData = JsonChangeData.Instance;
    }

    public void CheckAnswer(string number)
    {
        int squareIndex = gameObject.name[^2] - '0';
        if (!string.IsNullOrEmpty(number))
        {
            if (SceneButtonLogic2.storyMode == true)
            {
                if (number.Length > lastAttempt.Length &&
                !controller.rightNumbers[squareIndex].ToString().StartsWith(number))
                {
                    --controller.lifes;
                    controller.heartsControllers[controller.lifes].StopAnimation();
                    controller.hearts[controller.lifes].SetActive(false);

                    if (controller.lifes > 0)
                    {
                        controller.heartsControllers[controller.lifes - 1].StartAnimation();
                    }
                    controller.heartsExplosion[controller.lifes].SetActive(true);

                    if (controller.lifes <= 0)
                    {
                        controller.gameOver = true;
                        gameManager.GameOver();
                    }
                }
            }
                
            if (controller.rightNumbers[squareIndex] == Int32.Parse(number))
            {
                imageComponent.color = new Color(0.1f, 0.8f, 0.1f, 0.86f);
                controller.rightAnswers[squareIndex] = true;
                gameObject.GetComponent<TMP_InputField>().readOnly = true;
            }
            else
            {
                imageComponent.color = redColor;
                if (controller.rightAnswers[squareIndex])
                {
                    controller.rightAnswers[squareIndex] = false;
                }
            }

            lastAttempt = number;
        }
        else
        {
            imageComponent.color = redColor;
            if (controller.rightAnswers[squareIndex])
            {
                controller.rightAnswers[squareIndex] = false;
            }
        }

        int countRightAnswers = 0;
        foreach (bool answer in controller.rightAnswers)
        {
            if (answer)
            {
                ++countRightAnswers;
            }
        }

        if (countRightAnswers == controller.towerHeight)
        {
            if (SceneButtonLogic2.storyMode == false)
            {
                ++controller.currentLevel;
                StartCoroutine(ReloadLevel(1));
                controller.timer += 15;
            }
            else
            {
                //JSON logic
                if (SceneButtonLogic2.storyMode == true)
                {
                    modifyGameData.ModifyGameProgress("Chapter2_Level2", true);
                }
     
                //end scene logic
                SceneManager.LoadScene("EndScene_agn");
            }
        }
    }

    IEnumerator ReloadLevel(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        controller.BuildLevel();
    }
}