using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverVictoryUI;
    [SerializeField] private GameObject gameOverDefeatUI;

    public void GameOver()
    {
        gameOverDefeatUI.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("WelcomeScene");
    }

}