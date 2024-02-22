using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreTrigger : MonoBehaviour
{
    public ObjectDeterminator scoreSource;
    public TextMeshPro scoreText;
    private int score;
    private int playerScore;

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.layer == 3)
        {
            playerScore = PlayerScoreLogic.Instance.playerScore;
            if (ButtonController.Gamemode == 0 || 
                ButtonController.Gamemode == 1) score = scoreSource.score;
            if (ButtonController.Gamemode == 2 || 
                ButtonController.Gamemode == 3) score = Chap2Scoring(playerScore, scoreSource.score);
            PlayerScoreLogic.Instance.AddScore(score);
            HideObject();            
        }
    }
    private int Chap2Scoring(int baseScore, int additionalScore)
    {
        if(additionalScore > 0){
            return baseScore * (additionalScore - 1);
        }
        if(additionalScore < 0){
            return (baseScore/Math.Abs(additionalScore)) - baseScore;
        } 
        else return 0;       
    }

    private void HideObject()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        scoreText.GetComponent<MeshRenderer>().enabled = false;
    }
}
