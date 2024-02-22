using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random=UnityEngine.Random;
using TMPro;

public class ExitScoreLogic : MonoBehaviour
{
    public static ExitScoreLogic Instance;
    public TextMeshPro scoreText;
    private ObjectDeterminator obstacle;
    public GameObject[] obstacles;
    public int exitScore;

    void Awake()
    {
        Instance = this;
        exitScore = Random.Range(2, 21);
    }

    public void CalculateScore()
    {
        scoreText = GameObject.FindWithTag("Exit").transform.GetChild(0).gameObject.GetComponent<TextMeshPro>();
        if(ButtonController.Gamemode == 0 || ButtonController.Gamemode == 1) Chap1ScoreCalculator();
        if(ButtonController.Gamemode == 2 || ButtonController.Gamemode == 3) Chap2ScoreCalculator();
    }
    private void Chap1ScoreCalculator()
    {
        exitScore = 0;
        for (int i = 0; i < 4; i++)
        {
            if (GameObject.FindWithTag("Col" + i + "Obj") == null) break;

            obstacles = GameObject.FindGameObjectsWithTag("Col" + i + "Obj");
            obstacle = obstacles[Random.Range(0, obstacles.Length)].GetComponent<ObjectDeterminator>();
            exitScore += obstacle.score;
        }
        EndTrigger.Instance.exitScore = exitScore;
        scoreText.text = exitScore.ToString();        
    }
    private void Chap2ScoreCalculator()
    {
        obstacles = GameObject.FindGameObjectsWithTag("Col0Obj");
        obstacle = obstacles[Random.Range(0, obstacles.Length)].GetComponent<ObjectDeterminator>();

        if (obstacle.score < 0){
            PlayerScoreLogic.Instance.AddScore(Math.Abs(obstacle.score) * exitScore);
        }
        if (obstacle.score > 0){
            PlayerScoreLogic.Instance.AddScore(exitScore);
            exitScore *= obstacle.score;
        }
        EndTrigger.Instance.exitScore = exitScore;
        scoreText.text = exitScore.ToString();
        
        exitScore = Random.Range(2, 21);
    }
}
