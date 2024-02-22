using Enum = System.Enum;
using Array = System.Array;
using UnityEngine;
using TMPro;
using Operator = Enums.Operator;
using Random = UnityEngine.Random;
using System;
using System.Collections;

public class ControllerChapter3 : MonoBehaviour
{
    [SerializeField] private GameObject[] equation;

    public GameObject[] hearts;
    public HeartsAnimation[] heartsControllers;
    public GameObject[] heartsExplosion;
    [SerializeField] private GameManager gameManager;

    private TMP_InputField leftOperand;
    private TMP_Text eqOperator;
    private TMP_InputField centerOperand;
    private TMP_InputField rightOperand;
    private Operator @operator;
    public TMP_InputField requestedOperand;

    public string requestedNumber;
    private const int MaxNumber = 20;
    public byte lifes = 3;
    private int firstNumber;
    private int secondNumber;
    private int result;
    public bool gameOver = false;
    private TMP_Text levelText;

    [SerializeField] private GameObject levelTextObject;
    [SerializeField] private TMP_Text timerText;
    public float timer;
    public int currentLevel;

    private void Start()
    {
        currentLevel = 1;
        leftOperand = equation[0].GetComponent<TMP_InputField>();
        eqOperator = equation[1].GetComponent<TMP_Text>();
        centerOperand = equation[2].GetComponent<TMP_InputField>();
        rightOperand = equation[3].GetComponent<TMP_InputField>();

        if (SceneButtonLogic2.storyMode == false)
        {
            levelTextObject.SetActive(true);
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
        LoadLevel();
    }

    private void Update()
    {
        if (SceneButtonLogic2.storyMode == false)
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

    public void LoadLevel()
    {
        leftOperand.text = "";
        rightOperand.text = "";
        centerOperand.text = "";

        if (SceneButtonLogic2.storyMode == true)
        {
            heartsControllers = new HeartsAnimation[hearts.Length];
            for (int i = 0; i < hearts.Length; ++i)
            {
                heartsControllers[i] = hearts[i].GetComponent<HeartsAnimation>();
            }

            heartsControllers[^1].StartAnimation();
        }
        
        Array operatorElements = Enum.GetValues(typeof(Operator));
        @operator = (Operator)operatorElements.GetValue(Random.Range(0, operatorElements.Length));
        
        result = Random.Range(3, MaxNumber + 1);
        firstNumber = Random.Range(1, result);
        secondNumber = result - firstNumber;
        levelText = levelTextObject.GetComponent<TMP_Text>();
        levelText.text = "Level: " + currentLevel;

        ChangeLevelText();

        switch (Random.Range(0, 3))
        {
            case 0:
                requestedOperand = leftOperand;
                requestedNumber = ToRoman(firstNumber);
                leftOperand.readOnly = false;
                break;
            case 1:
                requestedOperand = centerOperand;
                requestedNumber = ToRoman(secondNumber);
                centerOperand.readOnly = false;
                break;
            case 2:
                requestedOperand = rightOperand;
                requestedNumber = ToRoman(result);
                rightOperand.readOnly = false;
                break;
        }

        if (@operator == Operator.Add)
        {
            eqOperator.text = "+";

            if (leftOperand.readOnly)
            {
                leftOperand.text = ToRoman(firstNumber);
            }

            if (centerOperand.readOnly)
            {
                centerOperand.text = ToRoman(secondNumber);
            }

            if (rightOperand.readOnly)
            {
                rightOperand.text = ToRoman(result);
            }
        }
        else
        {
            eqOperator.text = "-";

            if (leftOperand.readOnly)
            {
                leftOperand.text = ToRoman(result);
            }
            else
            {
                requestedNumber = ToRoman(result);
            }

            if (centerOperand.readOnly)
            {
                centerOperand.text = ToRoman(firstNumber);
            }
            else
            {
                requestedNumber = ToRoman(firstNumber);
            }

            if (rightOperand.readOnly)
            {
                rightOperand.text = ToRoman(secondNumber);
            }
            else
            {
                requestedNumber = ToRoman(secondNumber);
            }
        }

        requestedOperand.readOnly = true;
    }

    private static string ToRoman(int number)
    {
        return number switch
        {
            >= 50 => "L" + ToRoman(number - 50),
            >= 40 => "XL" + ToRoman(number - 40),
            >= 10 => "X" + ToRoman(number - 10),
            >= 9 => "IX" + ToRoman(number - 9),
            >= 5 => "V" + ToRoman(number - 5),
            >= 4 => "IV" + ToRoman(number - 4),
            >= 1 => "I" + ToRoman(number - 1),
            _ => ""
        };
    }

    private void ChangeLevelText()
    {
        levelText.text = "Level: " + currentLevel;
    }

}