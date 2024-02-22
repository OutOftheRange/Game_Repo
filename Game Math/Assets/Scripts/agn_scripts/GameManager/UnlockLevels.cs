using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UnlockLevels : MonoBehaviour
{
    private string filePath;

    public Button chapter1level2Button;
    public Button chapter1level3Button;
    public Button chapter2level1Button;
    public Button chapter2level2Button;
    public Button chapter2level3Button;
    public Button chapter3level1Button;
    public Button chapter3level2Button;
    public Button chapter3level3Button;
    public Button chapter4level1Button;
    public Button chapter4level2Button;
    public Button chapter4level3Button;
    public Button chapter5level1Button;
    public Button chapter5level2Button;
    public Button chapter6level1Button;

    private GameProgress gameProgress;

    void Start()
    {
        filePath = Path.Combine(Application.streamingAssetsPath, "gameProgress.json");
        LoadGameProgress();
        UpdateButtonStates();
    }

    void LoadGameProgress()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            gameProgress = JsonUtility.FromJson<GameProgress>(json);
        }
        else
        {
            Debug.LogError("gameProgress.json not found");
        }
    }

    void UpdateButtonStates()
    {
        if (gameProgress != null)
        {
            chapter1level2Button.interactable = gameProgress.Chapter1_Level2;
            chapter1level3Button.interactable = gameProgress.Chapter1_Level3;
            chapter2level1Button.interactable = gameProgress.Chapter2_Level1;
            chapter2level2Button.interactable = gameProgress.Chapter2_Level2;
            chapter2level3Button.interactable = gameProgress.Chapter2_Level3;
            chapter3level1Button.interactable = gameProgress.Chapter3_Level1;
            chapter3level2Button.interactable = gameProgress.Chapter3_Level2;
            chapter3level3Button.interactable = gameProgress.Chapter3_Level3;
            chapter4level1Button.interactable = gameProgress.Chapter4_Level1;
            chapter4level2Button.interactable = gameProgress.Chapter4_Level2;
            chapter4level3Button.interactable = gameProgress.Chapter4_Level3;
            chapter5level1Button.interactable = gameProgress.Chapter5_Level1;
            chapter5level2Button.interactable = gameProgress.Chapter5_Level2;
            chapter6level1Button.interactable = gameProgress.Chapter6_Level1;
        }
    }

    [System.Serializable]
    public class GameProgress
    {
        public bool Chapter1_Level2;
        public bool Chapter1_Level3;
        public bool Chapter2_Level1;
        public bool Chapter2_Level2;
        public bool Chapter2_Level3;
        public bool Chapter3_Level1;
        public bool Chapter3_Level2;
        public bool Chapter3_Level3;
        public bool Chapter4_Level1;
        public bool Chapter4_Level2;
        public bool Chapter4_Level3;
        public bool Chapter5_Level1;
        public bool Chapter5_Level2;
        public bool Chapter6_Level1;

        public GameProgress(bool chapter1_Level2, bool chapter1_Level3, bool chapter2_Level1, bool chapter2_Level2, bool chapter2_Level3, bool chapter3_Level1, bool chapter3_Level2, bool chapter3_Level3, bool chapter4_Level1, bool chapter4_Level2, bool chapter4_Level3, bool chapter5_Level1, bool chapter5_Level2, bool chapter6_Level1)
        {
            Chapter1_Level2 = chapter1_Level2;
            Chapter1_Level3 = chapter1_Level3;
            Chapter2_Level1 = chapter2_Level1;
            Chapter2_Level2 = chapter2_Level2;
            Chapter2_Level3 = chapter2_Level3;
            Chapter3_Level1 = chapter3_Level1;
            Chapter3_Level2 = chapter3_Level2;
            Chapter3_Level3 = chapter3_Level3;
            Chapter4_Level1 = chapter4_Level1;
            Chapter4_Level2 = chapter4_Level2;
            Chapter4_Level3 = chapter4_Level3;
            Chapter5_Level1 = chapter5_Level1;
            Chapter5_Level2 = chapter5_Level2;
            Chapter6_Level1 = chapter6_Level1;
        }
    }

}
