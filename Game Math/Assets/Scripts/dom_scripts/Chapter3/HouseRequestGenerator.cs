using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;
using TMPro;

public class HouseRequestGenerator : MonoBehaviour
{
    public static HouseRequestGenerator Instance;
    public int houseValue;
    private int maxRange = 11;
    private int minRange = 1;
    private int npcNumber;
    private int requestNumber;
    public int requestAmount {get {return requestCount;}}
    private int requestCount = 0;
    private TextMeshPro requestDisplay;
    [SerializeField] private Sprite[] romanSprites;
    [SerializeField] private SpriteRenderer npcRenderer;
    void Awake()
    {
        Instance = this;
        requestDisplay = transform.GetChild(0).GetComponent<TextMeshPro>();
        GenerateRequest();
    }

    public void GenerateRequest()
    {
        houseValue = Random.Range(minRange, maxRange);
        string[] requests = { 
            $"Proszę o dom wartości {houseValue}.",
            $"Zbuduj mi dom, gdzie {houseValue} pojawia się w kolumnach.",
            $"Czekam na dom wartości {houseValue}.",
            $"Dom z wartością {houseValue}... Możesz to dla mnie zrobić?",
            $"Słyszałem, że {houseValue} jest ostatnio w modzie."
        };
        requestNumber = Random.Range(0, 5);
        npcNumber = Random.Range(0, 4);

        npcRenderer.sprite = romanSprites[npcNumber];
        IncreaseDifficulty(requestCount);
        requestCount++;

        requestDisplay.text = requests[requestNumber];
        LevelLable.Instance.levelText.text = "Poziom:\n" + requestCount;

        if (requestCount == 15)
        {
            WinLossLogic.Instance.GameOver();
        }
    }

    private void IncreaseDifficulty(int counter)
    {
        switch(counter)
        {
            case 3:
                maxRange = 101;
                minRange = 10;
                break;
            case 8:
                maxRange = 501;
                minRange = 100;
                break;
            case 12:
                maxRange = 1001;
                minRange = 500;
                break;
        }
    }
}
