using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public Transform thisObstacle;
    private GameObject player;
    public float speed;
    public bool clicked;
    private static int columnNumber;
    private Animator animePlayer;

    void Start()
    {
        clicked = false;
        columnNumber = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        animePlayer = player.GetComponent<Animator>();
    }

    void OnMouseDown()
    {
        if (thisObstacle.tag == "Col" + columnNumber + "Obj" || 
            GameObject.FindGameObjectWithTag("Col" + columnNumber + "Obj") == null && 
            thisObstacle.tag == "Exit")
        {
            ObstacleIsClicked();
            foreach (GameObject lineClone in GameObject.FindGameObjectsWithTag("Line"))
            {
                Destroy(lineClone);
            }
            foreach (GameObject obstacle in GameObject.FindGameObjectsWithTag("Col" + columnNumber + "Obj")){
                obstacle.transform.GetChild(1).GetComponent<Collider2D>().enabled = false;
            }
        }
    }

    void Update() 
    {
        if (clicked)
        {
            float movingSpeed = speed * Time.deltaTime;
            player.transform.position = Vector3.MoveTowards(
                player.transform.position,
                thisObstacle.position,
                movingSpeed
            );
        }
        if (player.transform.position == thisObstacle.position && clicked == true)
        {
            columnNumber++;
            animePlayer.SetBool("ObstacleIsClicked", false);
            clicked = false;
            RenderNewLine();
            if (thisObstacle.tag == "Exit") EndTrigger.Instance.ExitTrigger();
        }
    }

    private void ObstacleIsClicked()
    {
        clicked = true;
        animePlayer.SetBool("ObstacleIsClicked", true);
    }

    private void RenderNewLine()
    {
        GameObject[] obstacleArray = GameObject.FindGameObjectsWithTag("Col" + columnNumber + "Obj");
        foreach (GameObject obstacle in obstacleArray)
        {
            SpawnBoard.Instance.CreateLine(obstacle.transform);
        }
        if (obstacleArray.Length == 0)
        {
            SpawnBoard.Instance.CreateLine(
                GameObject.FindGameObjectWithTag("Exit").transform);
        }
    }
}
