using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuLogic : MonoBehaviour
{
    private string targetScene = "WelcomeScene";
    public static int highScoreRecordChap1;
    public static int highScoreRecordChap2;
    public static int highScoreRecordChap3;
    public static int highScoreRecordChap4;
    public static int highScoreRecordChap5;
    private float transitionTime = 2.5f;
    public Animator animeTransition;

    public void Restart()
    {
        StartCoroutine(LoadLevel(WinLossLogic.nextScene));
        KeepHighScoreResetOther();
    }

    public void GoBackToMenu()
    {
        StartCoroutine(LoadLevel(targetScene));
        KeepHighScoreResetOther();
    }
    IEnumerator LoadLevel(string levelName){
        animeTransition.SetTrigger("LevelTransition");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelName);
    }

    private void KeepHighScoreResetOther()
    {
        switch(SceneManager.GetActiveScene().name)
        {
            case "Chap1&2Game1":
                if (ButtonController.Gamemode == 0 || 
                    ButtonController.Gamemode == 1) 
                    highScoreRecordChap1 = EndTrigger.CalculateHighScore();
                if (ButtonController.Gamemode == 2 || 
                    ButtonController.Gamemode == 3) 
                    highScoreRecordChap2 = EndTrigger.CalculateHighScore();
                break;
            case "Chap3Game1":
                
                break;
            case "Chap4Game1":

                break;
            case "Chap5Game1":
                highScoreRecordChap5 = HighScoreCalculator 
                (PowerCalculator.Instance.currentScore, highScoreRecordChap5);
                break;
        }
        ResetVariables();
    }

    private int HighScoreCalculator (int currentScore, int highScore)
    {
        if (currentScore > highScore) return currentScore;
        else return highScore;
    }
    
    private void ResetVariables()
    {
        switch(SceneManager.GetActiveScene().name)
        {
            case "Chap1&2Game1":
                EndTrigger.totalScore = 0;
                SpawnBoard.Instance.ResetDifficulty();
                LevelLable.Instance.SwitchTimer();
                break;
            case "Chap3Game1":
                
                break;
            case "Chap4Game1":
                NumberLineGenerator.Instance.Reset();
                break;
            case "Chap5Game1":
                PowerCalculator.Instance.Reset();
                break;
        }
    }
}
