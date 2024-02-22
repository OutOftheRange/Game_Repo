using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsLogic : MonoBehaviour
{
    public float endY = -8.0f;
    public float moveSpeed = 26.0f;
    public GameObject objectToMove;  

    IEnumerator MoveObject()
    {
        float startY = objectToMove.transform.position.y;

        while (objectToMove.transform.position.y > endY)
        {
            objectToMove.transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    //plot game menu
    public void PlotGameModeButton()
    {
        StartCoroutine(MoveObject());
        Invoke("ShowStartPlotScene", 1.0f);
    }

    //quit to desktop
    public void QuitGameButton()
    {
        Application.Quit();
    }

    //go back to welcome menu
    public void GoBackToWelcomeMenuButton()
    {
        StartCoroutine(MoveObject());
        Invoke("ShowWelcomeScene", 1.0f);
    }

    //exit game (agn)
    public void ExitGame()
    {
        StartCoroutine(MoveObject());
        Invoke("ShowWelcomeScene", 1.0f);
    }

    //from plot scene with king to plot mode
    public void ShowPlotMode()
    {
        StartCoroutine(MoveObject());
        Invoke("ShowPlotScene", 1.0f);
    }
    //endless mode
    public void ShowEndlessMode()
    {
        StartCoroutine(MoveObject());
        Invoke("ShowEndlessScene", 1.0f);
    }

    private void ShowWelcomeScene()
    {
        SceneManager.LoadScene("WelcomeScene");
    }

    private void ShowPlotScene()
    {
        SceneManager.LoadScene("PlotGameMode");
    }

    private void ShowStartPlotScene()
    {
        SceneManager.LoadScene("PlotStartScene");
    }

    private void ShowEndlessScene()
    {
        SceneManager.LoadScene("EndlessGameMode");
    }
}
