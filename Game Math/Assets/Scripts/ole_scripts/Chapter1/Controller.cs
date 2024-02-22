using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

public class Controller : MonoBehaviour
{
    [SerializeField] public int towerHeight;
    [SerializeField] private GameObject roof;
    [SerializeField] private GameObject levelTextObject;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private GameManager gameManager;
    [SerializeField] GameObject[] squares;
    [SerializeField] GameObject[] resultsInputFields;
    public float timer;
    public int currentLevel;

    private GameObject[,] cells;
    private TextMeshPro[,] equations;
    private TextMeshPro roofText;
    private TMP_Text levelText;

    private int[] leftNumbers;
    public int[] rightNumbers;
    public bool[] rightAnswers;
    private char[] signs;

    private int towerWidth = 2;
    private int maxResult = 20;
    private int minResult = 7;
    private int result;

    public byte lifes = 3;
    public GameObject[] hearts;
    public HeartsAnimation[] heartsControllers;
    public GameObject[] heartsExplosion;

    private void Start()
    {
        currentLevel = 1;

        if (SceneButtonLogic2.storyMode == false)
        {
            levelTextObject.gameObject.SetActive(true);
            timerText.gameObject.SetActive(true);
            timer = SceneButtonLogic2.timer;
        }
        else
        {
            foreach (GameObject heart in hearts)
            {
                heart.SetActive(true);
            }
        }
        
        
        buildLevel();
    }

    private void Update()
    {
        if(SceneButtonLogic2.storyMode == false)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                gameManager.GameOver();
            }

            TimeSpan timeSpan = TimeSpan.FromSeconds(timer);
            timerText.text = timeSpan.ToString(@"mm\:ss");
        }
       
    }

    public void buildLevel()
    {
        if (SceneButtonLogic2.storyMode == true)
        {
            heartsControllers = new HeartsAnimation[hearts.Length];
            for (int i = 0; i < hearts.Length; ++i)
            {
                heartsControllers[i] = hearts[i].GetComponent<HeartsAnimation>();
            }

            heartsControllers[^1].StartAnimation();
        }
        else
        {
            levelText = levelTextObject.GetComponent<TMP_Text>();
            ChangeLevelText();
        }

        rightAnswers = new bool[towerHeight];
        roofText = roof.GetComponentsInChildren<TextMeshPro>()[0];
        equations = new TextMeshPro[towerHeight, towerWidth];

        foreach (GameObject res in resultsInputFields)
        {
            res.GetComponent<TMP_InputField>().text = "";
            res.GetComponent<TMP_InputField>().readOnly = false;
            res.GetComponent<Image>().color = new Color(0, 0, 0, 0.8627f);
        }

        signs = new char[towerHeight];
        leftNumbers = new int[towerHeight];
        rightNumbers = new int[towerHeight];

        for (int i = 0; i < squares.Length; ++i)
        {
            equations[i / towerWidth, i % towerWidth] = squares[i].GetComponentsInChildren<TextMeshPro>()[0];
        }

        result = Random.Range(minResult, maxResult - 5);
        roofText.text = result.ToString();
        for (int i = 0; i < towerHeight; ++i)
        {
            int sign = Random.Range(0, 2);
            if (sign == 0)
            {
                signs[i] = '+';
                leftNumbers[i] = generateNewNumber(1, result, i);
            }
            else
            {
                signs[i] = '-';
                leftNumbers[i] = generateNewNumber(result + 1, maxResult, i);
            }

            rightNumbers[i] = Mathf.Abs(result - leftNumbers[i]);
            equations[i, 0].text = leftNumbers[i].ToString();
            equations[i, 1].text = signs[i].ToString();
        }
    }

    private int generateNewNumber(int leftBoundary, int rightBoundary, int index)
    {
        int tryNumber = Random.Range(leftBoundary, rightBoundary);
        for (int j = 0; j < index; ++j)
        {
            if (signs[j] == signs[index] && tryNumber == leftNumbers[j])
            {
                tryNumber = Random.Range(leftBoundary, rightBoundary);
                j = -1;
            }
        }
        return tryNumber;
    }

    private void ChangeLevelText()
    {
        levelText.text = "Level: " + currentLevel.ToString();
    }
}
