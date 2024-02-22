using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelLable : MonoBehaviour
{
    public static LevelLable Instance;
    public TextMeshProUGUI levelText;
    public float timeLeft = 90;
    private bool timerOn = true;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        Timer();
    }

    public void WriteToLable(int level)
    {
        levelText.text = "Poziom:\n" + level.ToString();
    }

    void Timer()
    {
        if ((ButtonController.Gamemode%2 == 1) && timerOn == true)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                UpdateTimer(timeLeft);
            }
            else
            {
                timeLeft = 0;
                timerOn = false;
                WinLossLogic.Instance.GameOver();
            }
        }
    }

    public void SwitchTimer()
    {
        if (timerOn == true) timerOn = false;
        else timerOn = true;
    }

    void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        levelText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

}
