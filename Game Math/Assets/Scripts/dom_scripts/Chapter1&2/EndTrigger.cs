using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    public static EndTrigger Instance;
    private GameObject player;
    public Collider2D exitCollider;
    public static int totalScore;
    private static int highScoreChap1;
    private static int highScoreChap2;
    public int exitScore;
    public Animator animeTransition;
    public Animator animePlayer;
    public Animator animeDoor;
    private bool win;
    private bool endingSequence;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        exitCollider = this.gameObject.transform.GetChild(1).GetComponent<Collider2D>();
    }

    void Update()
    {
        EndingPosition();
    }
    private void EndingPosition()
    {
        if (win == true && endingSequence == true){
            MovingThePlayer(6, 15);
        }
        if (win == false && endingSequence == true){
            MovingThePlayer(30, -15);
        }
    }
    public void MovingThePlayer(int speed, float x)
    {
        float movingSpeed = speed * Time.deltaTime;
        Vector3 target = new Vector3(x, 0f, 0f);
        player.transform.position = Vector2.MoveTowards(player.transform.position, target, movingSpeed);
        if (player.transform.position == target)
        {
            if (HPLogic.Instance.HPvalue > 0 &&
                SpawnBoard.levelCounter < 15) animeTransition.SetTrigger("NewBoard");
            player.transform.position = new Vector3(-15f, 0f, 0f);
            endingSequence = false;
        }
    }

    public void ExitTrigger()
    {
        DetermineEnding();
    }

    private void DetermineEnding()
    {
        if (PlayerScoreLogic.Instance.playerScore == exitScore) LevelWin();
        else LevelFail();
    }

    private void LevelWin()
    {
        win = true;
        animeDoor.SetTrigger("LevelComplete");
        animeDoor.SetBool("IsRendered", false);
        CalculateScore();
    }

    private void LevelFail()
    {
        win = false;
        animeDoor.SetTrigger("LevelFail");
    }

    private int CalculateScore()
    {
        if (ButtonController.Gamemode == 0 || ButtonController.Gamemode == 2){
            return totalScore += (SpawnBoard.Instance.amountOfColumns * SpawnBoard.Instance.totalAmountOfObstacles)/2;
        }
        if (ButtonController.Gamemode == 1){
            LevelLable.Instance.timeLeft += (SpawnBoard.Instance.amountOfColumns * SpawnBoard.Instance.totalAmountOfObstacles)/4;
            return totalScore += (SpawnBoard.Instance.amountOfColumns * SpawnBoard.Instance.totalAmountOfObstacles)/2;
        }
        if (ButtonController.Gamemode == 3){
            LevelLable.Instance.timeLeft += (exitScore * SpawnBoard.Instance.totalAmountOfObstacles)/4;
            return totalScore += (exitScore * SpawnBoard.Instance.totalAmountOfObstacles);
        }
        else return 0;
    }

    public static int CalculateHighScore()
    {
        if (ButtonController.Gamemode == 0 || ButtonController.Gamemode == 1){
            if (totalScore > highScoreChap1) return highScoreChap1 = totalScore;
            else return highScoreChap1;
        }
        if (ButtonController.Gamemode == 2 || ButtonController.Gamemode == 3){
            if (totalScore > highScoreChap2) return highScoreChap2 = totalScore;
            else return highScoreChap2;
        }
        else return 0;
    }

    IEnumerator AnimatePlayerWin(){
        yield return new WaitForSeconds(0.3f);
        animePlayer.SetBool("ObstacleIsClicked", true);
        yield return new WaitForSeconds(1f);
        endingSequence = true;
    }
    IEnumerator StatusCheck()
    {
        yield return new WaitForSeconds(2f);
        if ((ButtonController.Gamemode == 0) && 
            SpawnBoard.levelCounter == 15 &&
            HPLogic.Instance.HPvalue > 0) WinLossLogic.Instance.GameOver();

        if ((ButtonController.Gamemode == 2) && 
            SpawnBoard.levelCounter == 10 &&
            HPLogic.Instance.HPvalue > 0) WinLossLogic.Instance.GameOver();
    }
    private void AnimatePlayer()
    {
        if (win == true){
            animePlayer.SetTrigger("LevelWin");
            StartCoroutine(AnimatePlayerWin());
            StartCoroutine(StatusCheck());
        }
        else{
            animePlayer.SetTrigger("LevelFail");
            HPLogic.Instance.TakeDamage();
            endingSequence = true;
            StartCoroutine(StatusCheck());
        }
    }

}
