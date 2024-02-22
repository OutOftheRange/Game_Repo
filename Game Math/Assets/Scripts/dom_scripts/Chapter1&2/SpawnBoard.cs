using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class SpawnBoard : MonoBehaviour
{
    public static SpawnBoard Instance;
    public GameObject obstacle;
    public GameObject line;
    public GameObject player;
    public int amountOfColumns;
    public int amountOfObstacles;
    public int totalAmountOfObstacles;
    private double offsetX;
    private double offsetY;
    private double columnOffset = 2;
    private static int minimumRange = 2;
    private static int maximumRange = 2;
    public static int levelCounter = 0;
    public ObjectDeterminator randomObstacle;
    private bool isSpawned = false;
    public bool startingSequence = true;
    private int columnNumber = 0;
    private Animator animePlayer;
    private Animator animeDoor;

    void Awake()
    {
        animePlayer = player.GetComponent<Animator>();
        animeDoor = GameObject.FindGameObjectWithTag("Exit").GetComponent<Animator>();
        Instance = this;
    }
    void Start()
    {
        SpawnNewBoard();
    }

    void Update()
    {
        if (isSpawned == true)
        {
            StartCoroutine(UpdateScore());
        }
        StartingPosition();
    }
    
    private void StartingPosition()
    {
        if (startingSequence == true){
            float movingSpeed = 3 * Time.deltaTime;
            Vector3 target = new Vector3(-9f, 0f, 0f);

            animePlayer.SetBool("ObstacleIsClicked", true);
            player.transform.position = Vector2.MoveTowards(player.transform.position, target, movingSpeed);

            if (player.transform.position == target){
                startingSequence = false;
                animePlayer.SetBool("ObstacleIsClicked", false);
                foreach (GameObject obstacleClone in GameObject.FindGameObjectsWithTag("Col0Obj")){
                    CreateLine(obstacleClone.transform);
                    obstacleClone.transform.GetChild(1).GetComponent<Collider2D>().enabled = true;
                }
            }
        }
    }

    private void CreateBoard()
    {
        amountOfColumns = Random.Range(minimumRange, maximumRange);

        if (ButtonController.Gamemode == 0 || ButtonController.Gamemode == 1)
        {
            offsetX = -amountOfColumns - columnOffset;
            for (int i = 0; i < amountOfColumns; i++)
            {
                CreateObstaclesChap1(offsetX, i);
                offsetX += (((double)amountOfColumns + columnOffset) * 2d) / ((double)amountOfColumns - 1d);
            }
            IncreaseDifficultyChap1();
        }
        else if(ButtonController.Gamemode == 2 || ButtonController.Gamemode == 3)
        {
            CreateObstaclesChap2();
            IncreaseDifficultyChap2();
        }
        isSpawned = true;
    }
    private void CreateObstaclesChap1(double offsetX, int columnNumber)
    {
        amountOfObstacles = Random.Range(minimumRange - 1, maximumRange);
        totalAmountOfObstacles += amountOfObstacles;

        if (amountOfObstacles == 1) offsetY = 0;
        else offsetY = -amountOfObstacles;

        for (int i = 0; i < amountOfObstacles; i++)
        {
            GameObject obstacleInstance = Instantiate(obstacle, new Vector2((float)offsetX, (float)offsetY), transform.rotation);
            obstacleInstance.tag = "Col" + columnNumber.ToString() + "Obj";
            if (obstacleInstance.tag == "Col0Obj"){
                obstacleInstance.
                transform.GetChild(1).
                GetComponent<Collider2D>().
                enabled = false;
            }
            offsetY += ((double)amountOfObstacles * 2d) / ((double)amountOfObstacles - 1d);
        }
    }
    private void CreateObstaclesChap2()
    {
        amountOfObstacles = Random.Range(minimumRange - 1, maximumRange);
        totalAmountOfObstacles += amountOfObstacles;

        offsetX = 0;
        if (amountOfObstacles == 1) offsetY = 0;
        else offsetY = -amountOfObstacles;

        for (int i = 0; i < amountOfObstacles; i++)
        {
            GameObject obstacleInstance = Instantiate(obstacle, new Vector2((float)offsetX, (float)offsetY), transform.rotation);
            obstacleInstance.transform.GetChild(1).GetComponent<Collider2D>().enabled = false;
            obstacleInstance.tag = "Col0Obj";
            offsetY += ((double)amountOfObstacles * 2d) / ((double)amountOfObstacles - 1d);
        }
    }
    public void CreateLine(Transform obstacleInstancePosition) 
    {
        GameObject lineInstance = Instantiate(line);
        lineInstance.tag = "Line";
        LineController newLine = lineInstance.GetComponent<LineController>();
        newLine.SetUpLine(obstacleInstancePosition);
    }
    private void IncreaseDifficultyChap1()
    {
        if (maximumRange < 5 && levelCounter !=0)
        {
            if (levelCounter % 4 == 0) maximumRange++;
            if (levelCounter % 8 == 0) minimumRange++;
        }
        if (levelCounter == 0) maximumRange++;

        levelCounter++;
    }
    private void IncreaseDifficultyChap2(){
        if (maximumRange < 5 && levelCounter !=0)
        {
            if (levelCounter % 3 == 0) maximumRange++;
            if (levelCounter % 6 == 0) minimumRange++;
        }
        if (levelCounter == 0) minimumRange = ++maximumRange;

        levelCounter++;
    }
    public void ResetDifficulty()
    {
        maximumRange = 2;
        minimumRange = 2;
        levelCounter = 0;
    }
    IEnumerator UpdateScore()
    {
        yield return new WaitForEndOfFrame();
        randomObstacle = GameObject.FindWithTag("Col" + 0 + "Obj").GetComponent<ObjectDeterminator>();
        if (randomObstacle.score != 0)
        {   
            PlayerScoreLogic.Instance.ZeroOutScore();
            ExitScoreLogic.Instance.CalculateScore();
            isSpawned = false;
        }
        yield return null;
    }
    public void SpawnNewBoard()
    {
        do
        {
            foreach (GameObject obstacleClone in GameObject.FindGameObjectsWithTag("Col" + columnNumber + "Obj"))
            {
                obstacleClone.GetComponent<ObjectDeterminator>().score = 0;
                Destroy(obstacleClone);               
            }
            columnNumber++;
        } while (GameObject.FindGameObjectWithTag("Col" + columnNumber + "Obj"));
        columnNumber = 0;
        totalAmountOfObstacles = 0;
        startingSequence = true;
        animeDoor.SetBool("IsRendered", true);
        CreateBoard();
        AssignLevel();
    }
    public void AssignLevel()
    {
        LevelLable.Instance.levelText.text = "Poziom:\n" + levelCounter.ToString();
    }
}

