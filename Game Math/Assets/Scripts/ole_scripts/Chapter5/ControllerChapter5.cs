using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

enum SignDirection
{
    Left = 0,
    Right = 1
}

public class ControllerChapter5 : MonoBehaviour
{
    [SerializeField] private TMP_InputField[] signs = new TMP_InputField[2];
    [SerializeField] private TMP_Text[] hps = new TMP_Text[2];
    [SerializeField] private Image enemyHpBar;
    [SerializeField] private GameObject[] trolls = new GameObject[2];
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject fireWork;
    [SerializeField] private Animator[] trollsAnimation;

    [SerializeField] private GameObject levelTextObject;
    [SerializeField] private TMP_Text timerText;

    public GameObject[] hearts;
    public HeartsAnimation[] heartsControllers;
    public GameObject[] heartsExplosion;

    public float timer;
    public int currentLevel;

    private int[] answers = new int[2];
    private string[] stringAnswers = new string[2];
    private int[] hp = new int[2];
    private bool[] isAnswered = new bool[2];
    private short totalStages = 3;
    private short ourHp;
    private short enemyHp;

    private Transform[] trollsTransform;  
    private TMP_Text levelText;

    //JSON logic
    private JsonChangeData modifyGameData;

    private void Start()
    {
        //JSON logic
        modifyGameData = JsonChangeData.Instance;

        currentLevel = 1;
        trollsTransform = new Transform[2];

        for (int i = 0; i < trolls.Length; ++i)
        {
            trollsTransform[i] = trolls[i].transform;
            Debug.Log(trollsTransform[i].position);
        }

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
        GenerateTask();

        if (SceneButtonLogic2.storyMode == true)
        {
            heartsControllers = new HeartsAnimation[hearts.Length];
            for (int i = 0; i < hearts.Length; ++i)
            {
                heartsControllers[i] = hearts[i].GetComponent<HeartsAnimation>();
            }

            heartsControllers[^1].StartAnimation();
        }

        ourHp = 3;
        enemyHp = totalStages;

        signs[0].onValueChanged.AddListener(s => { OnInputAnswer(SignDirection.Left, s); });
        signs[1].onValueChanged.AddListener(s => { OnInputAnswer(SignDirection.Right, s); });
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

    private void RebuildLevel()
    {
        ourHp = 3;
        enemyHp = totalStages;
        GenerateTask();

        trollsAnimation[0].Play("run", -1, 0f);
        trollsAnimation[0].Play("Troll run", -1, 0f);
        trollsAnimation[0].Play("Troll rotation", -1, 0f);

        trollsAnimation[1].Play("run", -1, 0f);
        trollsAnimation[1].Play("Troll2 run", -1, 0f);
        trollsAnimation[1].Play("Troll2 rotation", -1, 0f);
        
        for (int i = 0; i < trolls.Length; ++i)
        {
            trollsAnimation[i].SetBool("isDead", false);
        }
        enemyHpBar.fillAmount = 1.0f;

        signs[0].text = "";
        signs[1].text = "";
    }

    private void PrintSigns()
    {
        hps[0].text = hp[0].ToString();
        hps[1].text = hp[1].ToString();
    }

    private void GenerateTask()
    {
        hp[0] = Random.Range(1, 31);
        hp[1] = Random.Range(1, 31);

        if (SceneButtonLogic2.storyMode == false)
        {
            levelText = levelTextObject.GetComponent<TMP_Text>();
            ChangeLevelText();
        }
            
        answers[0] = NWD(hp[0], hp[1]);
        //NWW
        answers[1] = hp[0] * hp[1] / answers[0];
        stringAnswers[0] = answers[0].ToString();
        stringAnswers[1] = answers[1].ToString();

        PrintSigns();
        isAnswered[0] = false;
        isAnswered[1] = false;
    }

    private int NWD(int a, int b)
    {
        if (a < b)
        {
            (a, b) = (b, a);
        }

        while (b > 0)
        {
            a %= b;
            (a, b) = (b, a);
        }

        return a;
    }

    private void OnInputAnswer(SignDirection signDirection, string inputValue)
    {
        if (stringAnswers[(int)signDirection].Equals(inputValue))
        {
            isAnswered[(int)signDirection] = true;
            //particulary correct 
        }
        else if (stringAnswers[(int)signDirection].StartsWith(inputValue))
        {
            //correct
        }
        else
        {
            //incorrect option
            if (SceneButtonLogic2.storyMode == true)
            {
                DecreaseOurHp();
            }

        }

        CheckIfIOrEnemyHaveZeroHp();
    }

    private void DecreaseOurHp()
    {
        --ourHp;
        heartsControllers[ourHp].StopAnimation();
        hearts[ourHp].SetActive(false);
        if (ourHp > 0)
        {
            heartsControllers[ourHp - 1].StartAnimation();
        }

        heartsExplosion[ourHp].SetActive(true);
    }

    private void CheckIfIOrEnemyHaveZeroHp()
    {
        if (ourHp == 0)
        {
            StartCoroutine(RunFireWork());
        }

        for(int i = 0; i < stringAnswers.Length; ++i)
        {
            Debug.Log(stringAnswers[i]); 
        }

        ushort rightAnswers = 0;
        foreach (bool result in isAnswered)
        {
            if (result)
            {
                ++rightAnswers;
            }
        }

        if (rightAnswers == isAnswered.Length)
        {
            // minus enemy hp and start new task

            --enemyHp;
            enemyHpBar.fillAmount -= 1.0f / totalStages;
            if (Math.Abs(enemyHpBar.fillAmount) < 1e-3)
            {
                enemyHpBar.fillAmount = 0;
            }
             
            if (enemyHp == 0)
            {

                foreach (GameObject troll in trolls)
                {
                    troll.GetComponent<Animator>().SetBool("isDead", true);
                }

                if (SceneButtonLogic2.storyMode == true)
                {
                    StartCoroutine(RunFireWork());

                    //end scene logic
                    if (SceneButtonLogic2.storyMode == true)
                    {
                        SceneManager.LoadScene("PlotEndScene");
                    }
                }
                else
                {
                    ++currentLevel;
                    timer += 25;

                    RebuildLevel();
                }
            }
            else
            {
                GenerateTask();
                signs[0].text = "";
                signs[1].text = "";
            }
        } 
    }

    private IEnumerator RunFireWork()
    {
        fireWork.SetActive(true);
        yield return new WaitForSeconds(1);
        gameManager.GameOver();
    
    }

    private void ChangeLevelText()
    {
        levelText.text = "Level: " + currentLevel;
    }
}