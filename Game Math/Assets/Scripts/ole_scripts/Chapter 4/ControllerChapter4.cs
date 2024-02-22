using System.Collections;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControllerChapter4 : MonoBehaviour
{
    [SerializeField] private UnityEngine.Object[] stagesObjects;
    [SerializeField] private TMP_Text[] knownStages;
    [SerializeField] private int[] knownStagesIndexes;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject fireWork;
    [SerializeField] private TMP_InputField[] inputFields;
    [SerializeField] public int stagesNumber = 7;
    [SerializeField] private GameObject train;

    public GameObject[] hearts;
    public HeartsAnimation[] heartsControllers;
    public GameObject[] heartsExplosion;

    public int[] stages;
    public byte lifes = 3;
    private const int NumbersBorderRange = 30;
    private const int NumbersRange = 20;
    private Color inactiveColor = new Color(0.42f, 0.13f, 0.13f, 0.71f);
    private Color activeColor = new Color(0f, 0f, 0f, 0.86f);
    private Image[] unknownStagesImages;

    public int trainBorder = 0;
    public int currentStage = 0;

    public int currentStageInAllStages = 0;
    private int curKnownIndex = 0;

    public bool gameOver = false;

    [SerializeField] private TMP_Text timerText;
    public float timer;
    public int currentLevel;
    [SerializeField] private GameObject levelTextObject;
    private TMP_Text levelText;

    //JSON logic
    private JsonChangeData modifyGameData;

    private void Start()
    {
        //JSON logic
        modifyGameData = JsonChangeData.Instance;

        if (SceneButtonLogic2.storyMode == false)
        {
            levelTextObject.gameObject.SetActive(true);
            timerText.gameObject.SetActive(true);
            timer = SceneButtonLogic2.timer;

            levelText = levelTextObject.GetComponent<TMP_Text>();
            levelText.text = "Level: " + currentLevel;
            ChangeLevelText();
        }
        else
        {
            foreach (GameObject heart in hearts)
            {
                heart.SetActive(true);
            }
        }

        setStages();

        if (SceneButtonLogic2.storyMode == true)
        {
            heartsControllers = new HeartsAnimation[hearts.Length];
            for (int i = 0; i < hearts.Length; ++i)
            {
                heartsControllers[i] = hearts[i].GetComponent<HeartsAnimation>();
            }

            heartsControllers[^1].StartAnimation();
        }
        SetTrainBorder();
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

    public void SetTrainBorder()
    {
        if (knownStagesIndexes[curKnownIndex] == currentStageInAllStages + 1)
        {
            ++currentStageInAllStages;
        }

        if (knownStagesIndexes[curKnownIndex] == currentStageInAllStages)
        {
            while (curKnownIndex < knownStagesIndexes.Length &&
                   knownStagesIndexes[curKnownIndex] == currentStageInAllStages)
            {
                ++curKnownIndex;
                if (curKnownIndex + 1 < knownStagesIndexes.Length &&
                    knownStagesIndexes[curKnownIndex + 1] == currentStageInAllStages + 1)
                {
                    ++currentStageInAllStages;
                }
            }

            trainBorder = (int)stagesObjects[currentStageInAllStages].GetComponent<Transform>().localPosition.x;
        }
        else
        {
            trainBorder = (int)stagesObjects[currentStageInAllStages].GetComponent<Transform>().localPosition.x;
        }

        if (SceneButtonLogic2.storyMode == true && currentStageInAllStages == stagesNumber - 1)
        {
            StartCoroutine(RunFireWork(1));
            //JSON logic
            if (SceneButtonLogic2.storyMode == true)
            {
                modifyGameData.ModifyGameProgress("Chapter4_Level2", true);
            }
            SceneManager.LoadScene("EndScene_agn");
        }

        if (SceneButtonLogic2.storyMode == false && currentStageInAllStages == stagesNumber - 1)
        {
            StartCoroutine(ReloadLevel(8));
        }

    }

    private void setStages(){
        stages = new int[stagesNumber];
        int startStage = Random.Range(-NumbersBorderRange, NumbersBorderRange + 1);
        int range = Random.Range(-NumbersRange, NumbersRange + 1);

        for (int i = 0; i<stagesNumber; ++i)
        {
            stages[i] = startStage;
            startStage += range;
        }

        for (int i = 0; i < knownStages.Length; ++i)
        {
            knownStages[i].text = stages[knownStagesIndexes[i]].ToString();
        }
    }

    private IEnumerator RunFireWork(float delayTime)
    {
        fireWork.SetActive(true);
        yield return new WaitForSeconds(delayTime);
        gameManager.GameOver();
    }

    private void ChangeLevelText()
    {
        levelText.text = "Level: " + currentLevel;
    }

    IEnumerator ReloadLevel(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        ++currentLevel;
        timer += 45;
        trainBorder = 0;
        currentStage = 0;
        currentStageInAllStages = 0;
        curKnownIndex = 0;
        train.transform.localPosition = new Vector3(-892, train.transform.localPosition.y, train.transform.localPosition.z);
        setStages();
        SetTrainBorder();
        ResetInputTextFields();
        ChangeLevelText();
    }

    private void ResetInputTextFields()
    {
        unknownStagesImages = new Image[inputFields.Length];

        for (int i = 0; i<inputFields.Length; ++i) 
        {
            Debug.Log(inputFields[i].text);
            inputFields[i].text = "";
            unknownStagesImages[i] = inputFields[i].GetComponent<Image>();
            unknownStagesImages[i].color = inactiveColor;
        }

        inputFields[currentStage].enabled = true;
        unknownStagesImages[currentStage].color = activeColor;
    }
}
