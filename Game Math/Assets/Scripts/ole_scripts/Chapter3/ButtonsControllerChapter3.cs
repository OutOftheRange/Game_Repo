using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ButtonsControllerChapter3 : MonoBehaviour
{
    [SerializeField] private GameObject controllerObject;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject fireWork;

    private ControllerChapter3 controller;
    private string buttonText;

    //JSON logic
    private JsonChangeData modifyGameData;

    private void Start()
    {
        controller = controllerObject.GetComponent<ControllerChapter3>();
        buttonText = GetComponentInChildren<TMP_Text>().text;
        //JSON logic
        modifyGameData = JsonChangeData.Instance;
    }

    public void Press()
    {
        if (controller.gameOver) return;

        controller.requestedOperand.text += buttonText;
        if (SceneButtonLogic2.storyMode == true)
        {
            if (!controller.requestedNumber.StartsWith(controller.requestedOperand.text))
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
            
        if (!string.IsNullOrEmpty(controller.requestedOperand.text) && controller.requestedOperand.text == controller.requestedNumber)
        {
            if (SceneButtonLogic2.storyMode == true)
            {
                controller.gameOver = true;
                StartCoroutine(RunFireWork(1));
                //JSON logic
                if (SceneButtonLogic2.storyMode == true)
                {
                    modifyGameData.ModifyGameProgress("Chapter3_Level2", true);
                }
                    
                //end scene logic
                SceneManager.LoadScene("EndScene_agn");
            }
            else
            {
                ++controller.currentLevel;
                StartCoroutine(ReloadLevel(1));
                controller.timer += 15;
            }   
        }
    }

    public void Erase()
    {
        if (!string.IsNullOrEmpty(controller.requestedOperand.text))
        {
            controller.requestedOperand.text = controller.requestedOperand.text.Substring(0, controller.requestedOperand.text.Length - 1);
        }
    }

    private IEnumerator RunFireWork(float delayTime)
    {
        fireWork.SetActive(true);
        yield return new WaitForSeconds(delayTime);
        gameManager.GameOver();
    }

    IEnumerator ReloadLevel(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        controller.requestedOperand.text = "";
        controller.LoadLevel();
    }
}