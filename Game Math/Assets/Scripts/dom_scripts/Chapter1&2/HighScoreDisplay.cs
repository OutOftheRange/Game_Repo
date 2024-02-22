using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreDisplay : MonoBehaviour
{
    private static TextMeshProUGUI highScoreChap1;
    private static TextMeshProUGUI highScoreChap2;
    public TextMeshProUGUI labelChap1;
    public TextMeshProUGUI labelChap2;

    public void Awake()
    {
        highScoreChap1 = labelChap1;
        highScoreChap2 = labelChap2;
        SetHighScore();
    }

    public static void SetHighScore()
    {
        highScoreChap1.text = "High Score:\n" + GameOverMenuLogic.highScoreRecordChap1;
        highScoreChap2.text = "High Score:\n" + GameOverMenuLogic.highScoreRecordChap2;
    }
}


