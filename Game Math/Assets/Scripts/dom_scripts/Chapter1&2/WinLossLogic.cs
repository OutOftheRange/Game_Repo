using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WinLossLogic : MonoBehaviour
{
    public static WinLossLogic Instance;
    public Animator animeUI;
    public Animator animePlayer;
    [SerializeField] private GameObject gameOverScreen;
    private TextMeshProUGUI endingMessage;
    private TextMeshProUGUI finalScore;
    private TextMeshProUGUI buttonLable;
    public static string nextScene;

    //JSON logic
    private JsonChangeData modifyGameData;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        endingMessage = gameOverScreen.transform.Find("EndingMessage").GetComponent<TextMeshProUGUI>();
        finalScore = gameOverScreen.transform.Find("FinalScore").GetComponent<TextMeshProUGUI>();
        buttonLable = gameOverScreen.transform.Find("TryAgain").GetChild(0).GetComponent<TextMeshProUGUI>();

        //JSON logic
        modifyGameData = JsonChangeData.Instance;
    }

    public void GameOver()
    {
        Debug.Log("GameOver");
        switch(SceneManager.GetActiveScene().name)
        {
            case "Chap1&2Game1":
                Chapter1n2();
                break;
            case "Chap3Game1":
                Chapter3();
                break;
            case "Chap4Game1":
                Chapter4();
                break;
            case "Chap5Game1":
                Chapter5();
                break;
        }
    }

    private void Chapter1n2()
    {
        EndTrigger.Instance.exitCollider.enabled = false;
        LevelLable.Instance.SwitchTimer();
        animePlayer.SetBool("ObstacleIsClicked", false);
        animeUI.SetTrigger("GameIsOver");
        gameOverScreen.SetActive(true);
        finalScore.text = "Ostateczny wynik:\n" + EndTrigger.totalScore;
        if ((ButtonController.Gamemode == 0 && HPLogic.Instance.HPvalue > 0 && SpawnBoard.levelCounter >= 15) ||
            (ButtonController.Gamemode == 2 && HPLogic.Instance.HPvalue > 0 && SpawnBoard.levelCounter >= 10))
        {
            //win
            endingMessage.text = "Poziom Zakończony";
            endingMessage.fontSize = 40;
            nextScene = "Chap1&2Game1";
            buttonLable.text = "Powtórz poziom";

            //JSON logic
            if (ButtonController.Gamemode == 0)
            {
                modifyGameData.ModifyGameProgress("Chapter1_Level3", true);
            }

            if (ButtonController.Gamemode == 2)
            {
                modifyGameData.ModifyGameProgress("Chapter2_Level3", true);
            }
        }
        else
        {
            //failure
            endingMessage.text = "Koniec Gry";
            nextScene = SceneManager.GetActiveScene().name;
        }
    }

    private void Chapter3()
    {
        LevelLable.Instance.SwitchTimer();
        animeUI.SetTrigger("GameIsOver");
        gameOverScreen.SetActive(true);
        finalScore.text = "Ostateczny wynik:\n" + HouseLogic.totalScore;
        if (ButtonController.Gamemode == 0 && HPLogic.Instance.HPvalue > 0 && HouseRequestGenerator.Instance.requestAmount >= 15)
        {
            //win
            endingMessage.text = "Poziom Zakończony";
            endingMessage.fontSize = 40;
            nextScene = "Chap3Game1";
            buttonLable.text = "Powtórz poziom";

            //JSON logic
            modifyGameData.ModifyGameProgress("Chapter3_Level3", true);
        }
        else
        {
            //failure
            endingMessage.text = "Koniec Gry";
            nextScene = SceneManager.GetActiveScene().name;
        }
    }

    private void Chapter4()
    {
        LevelLable.Instance.SwitchTimer();
        animeUI.SetTrigger("GameIsOver");
        gameOverScreen.SetActive(true);
        finalScore.text = "Ostateczny wynik:\n" + NumberLineGenerator.Instance.currentScore;
        NumberLineGenerator.Instance.DeactivateMarks();
        if (ButtonController.Gamemode == 0 && HPLogic.Instance.HPvalue > 0 && NumberLineGenerator.Instance.level >= 15)
        {
            //win
            endingMessage.text = "Poziom Zakończony";
            endingMessage.fontSize = 40;
            nextScene = "Chap4Game1";
            buttonLable.text = "Powtórz poziom";

            //JSON logic
            modifyGameData.ModifyGameProgress("Chapter4_Level3", true);
        }
        else
        {
            //failure
            endingMessage.text = "Koniec Gry";
            nextScene = SceneManager.GetActiveScene().name;
        }
    }

    private void Chapter5()
    {
        LevelLable.Instance.SwitchTimer();
        animeUI.SetTrigger("GameIsOver");
        gameOverScreen.SetActive(true);
        finalScore.text = "Ostateczny wynik:\n" + PowerCalculator.Instance.currentScore;
        if (ButtonController.Gamemode == 0 && HPLogic.Instance.HPvalue > 0 && PowerCalculator.level >= 25)
        {
            //win
            endingMessage.text = "Poziom Zakończony";
            endingMessage.fontSize = 40;
            nextScene = "Chap5Game1";
            buttonLable.text = "Powtórz poziom";

            //JSON logic
            modifyGameData.ModifyGameProgress("Chapter5_Level2", true);
        }
        else
        {
            //failure
            endingMessage.text = "Koniec Gry";
            nextScene = SceneManager.GetActiveScene().name;
        }
    }

}
