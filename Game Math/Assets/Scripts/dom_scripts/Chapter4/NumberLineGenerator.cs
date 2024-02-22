using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class NumberLineGenerator : MonoBehaviour
{
    public static NumberLineGenerator Instance { get {return _instance;}}
    private static NumberLineGenerator _instance;
    private int[] numberLineValues = new int[15];
    private int numberLineScale;
    public int GetNumberLineScale() => numberLineScale;
    private GameObject[] numberLineMarks = new GameObject[15];
    [SerializeField] private GameObject numberMark;
    private int marksAmount = 15;
    private double offsetX = -8;
    private double offsetY = 0.1;
    private static int minScaleRange = 1;
    private static int maxScaleRange = 1;
    private int minRandRange = 3;
    private int maxRandRange = 5;
    private List<int> invisibleMarks;
    public int GetInvisibleAmount() => invisibleMarks.Count;
    private int target;
    public int GetTarget() => target;
    public int level { get {return _level;}}
    private static int _level = 1;
    public int currentScore { get {return _currentScore;}}
    private static int _currentScore = 0;
    private Animator animeLine;

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        animeLine = transform.GetComponent<Animator>();
        invisibleMarks = new List<int>();
        CreateNumberLine();
    }

    private void CreateNumberLine()
    {
        DecideValues();

        for (int i = 0; i < marksAmount; i++)
        {
            GameObject numberMarkInstance = Instantiate(numberMark, new Vector2((float)offsetX, (float)offsetY), transform.rotation, transform);
            numberMarkInstance.transform.localPosition = new Vector2((float)offsetX, (float)offsetY);
            numberMarkInstance.GetComponent<NumberMarkLogic>().value = numberLineValues[i];
            numberMarkInstance.name = "NumberMark" + i;
            if (invisibleMarks.Contains(numberLineValues[i])) numberMarkInstance.GetComponent<NumberMarkLogic>().isInvisible = true;

            numberLineMarks[i] = numberMarkInstance;
            offsetX++;
        }
        LevelLable.Instance.WriteToLable(_level);
    }

    public void RegenerateNumberLine()
    {
        _currentScore += target/2 * numberLineScale * 2;
        DecideValues();
        IncreaseDifficultyChap3();
        for (int i = 0; i < marksAmount; i++)
        {
            numberLineMarks[i].GetComponent<NumberMarkLogic>().value = numberLineValues[i];
            if (invisibleMarks.Contains(numberLineValues[i])) numberLineMarks[i].GetComponent<NumberMarkLogic>().isInvisible = true;
            else numberLineMarks[i].GetComponent<NumberMarkLogic>().isInvisible = false;
            numberLineMarks[i].GetComponent<NumberMarkLogic>().GenerateMark();
        }
        _level++;
        LevelLable.Instance.WriteToLable(_level);
        if (_level == 15 && ButtonController.Gamemode == 0 && HPLogic.Instance.HPvalue > 0) 
        {
            WinLossLogic.Instance.GameOver();
        }
    }

    private void DecideValues()
    {
        invisibleMarks.Clear();
        do{
            numberLineScale = Random.Range(minScaleRange,maxScaleRange);
        } while(numberLineScale==0);

        for (int i = 0; i < marksAmount; i++)
        {
            numberLineValues[i] = numberLineScale * i;
        }

        int invisibleAmount = Random.Range(minRandRange,maxRandRange);

        for (int i = 0; i < invisibleAmount; i++)
        {
            bool next = false;
            do
            {
                int invisibleValue = numberLineValues[Random.Range(1,15)];
                if (!invisibleMarks.Contains(invisibleValue)) {
                    invisibleMarks.Add(invisibleValue);
                    next = true;
                }
            } while(!next);
        }

        target = invisibleMarks[Random.Range(0,invisibleMarks.Count)];
        TargetLableLogic.Instance.WriteTarget(target);
    }

    private void IncreaseDifficultyChap3()
    {
        if (_level%3==0 && level<=15)
        {
            maxScaleRange += 2;
            minScaleRange -= 2;
            maxRandRange += 2;
            minRandRange++;
        }
        else if (_level%5==0 && _level>15 && minRandRange<14)
        {
            maxScaleRange++;
            minScaleRange--;
            minRandRange++;
        }
        else{
            maxScaleRange++;
            minScaleRange--;            
        }
    }

    public void DeactivateMarks()
    {
        foreach (var mark in numberLineMarks)
        {
            mark.SetActive(false);
        }
    }
    
    public void EnableAnimation()
    {
        animeLine.enabled = true;
    }

    public void Reset()
    {
        _level = 1;
        maxScaleRange = 1;
        minScaleRange = 1;
        maxRandRange = 5;
        minRandRange = 3;
        animeLine.enabled = false;
    }
}
