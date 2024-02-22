using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScoreLogic : MonoBehaviour
{
    public static PlayerScoreLogic Instance { get {return _instance;}}
    private static PlayerScoreLogic _instance;
    public int playerScore;
    public TextMeshPro scoreText;

    private void Awake()
    {
        _instance = this;
    }

    void Update(){
    }

    public void AddScore(int scoreToAdd)
    {
        playerScore += scoreToAdd;
        scoreText.text = playerScore.ToString();
    }
    public void ZeroOutScore()
    {
        playerScore = 0;
        scoreText.text = playerScore.ToString();
    }
}
