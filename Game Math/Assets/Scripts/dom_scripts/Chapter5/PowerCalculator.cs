using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PowerCalculator : MonoBehaviour
{
    public static PowerCalculator Instance { get {return _instance;}}
    private static PowerCalculator _instance;
    private SpriteRenderer enemySprite;
    [SerializeField] private Sprite[] enemyApperances;
    private int exponent;
    private int baseNumber;
    private int baseNumberMin = 2;
    private int baseNumberMax = 5;
    private int lastNumber = 0;
    private int power;
    private TextMeshProUGUI powerValue;
    private TextMeshProUGUI baseValue;
    public static int level { get {return _level;}}
    private static int _level = 1;
    public int currentScore { get {return _currentScore;}}
    private int _currentScore;

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        powerValue = gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        baseValue = gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        enemySprite = gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>();
        LevelLable.Instance.WriteToLable(level); 
        GeneratePower();
    }

    private void GeneratePower()
    {
        exponent = UnityEngine.Random.Range(2,4);
        do
        {
            baseNumber = UnityEngine.Random.Range(baseNumberMin, baseNumberMax);
        } while (baseNumber == lastNumber);
        lastNumber = baseNumber;
        baseValue.text = baseNumber.ToString() + "<sup>x</sup>";

        power = (int)Math.Pow(baseNumber, exponent);
        powerValue.text = power.ToString();

        enemySprite.sprite = enemyApperances[UnityEngine.Random.Range(0,3)];
    }

    public void CheckRoot(int exponent)
    {
        if ((Math.Sqrt(power) == baseNumber && exponent == 2) || (Math.Cbrt(power) == baseNumber && exponent == 3))
        {

            _currentScore += power/(baseNumber/2);
            GeneratePower();
            Debug.Log("Correct");
            _level++;
            if (level == 26 && 
            ButtonController.Gamemode == 0 && 
            HPLogic.Instance.HPvalue > 0) WinLossLogic.Instance.GameOver();
            else
            {
                IncreaseDifficultyChap5(level);
                LevelLable.Instance.WriteToLable(level);                
            }
          
        }
        else
        {
            HPLogic.Instance.TakeDamage();
            Debug.Log("Incorrect");
        }
    }

    private void IncreaseDifficultyChap5(int levelCounter)
    {
        if (baseNumberMax < 15 && levelCounter !=0)
        {
            if (levelCounter % 3 == 0) baseNumberMax++;
            if (levelCounter % 6 == 0) baseNumberMin++;
        }
        else if (levelCounter % 10 == 0)
        {
            baseNumberMax++;
            baseNumberMin++;
        }
    }
    

    public void Reset()
    {
        _currentScore = 0;
        _level = 1;
    }

}
