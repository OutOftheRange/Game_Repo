using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random=UnityEngine.Random;
using TMPro;

public class ObjectDeterminator : MonoBehaviour
{
    public SpriteRenderer mySprite;
    public Sprite[] evilObstacleSprite;
    public Sprite[] goodObstacleSprite;
    public List<int> excludedScores;
    public int score;
    private int spriteRng;
    public TextMeshPro value;


    void Start()
    {
        PrepareObjectData();
    }
    void PrepareObjectData()
    {
        foreach (GameObject sameColumnItem in GameObject.FindGameObjectsWithTag(this.tag)){
            excludedScores.Add(sameColumnItem.GetComponent<ObjectDeterminator>().score);
        }
        excludedScores.RemoveAll(listValue => listValue == 0);
        excludedScores.Add(0);

        if(ButtonController.Gamemode == 0 || ButtonController.Gamemode == 1) Chapter1Values();
        if(ButtonController.Gamemode == 2 || ButtonController.Gamemode == 3) Chapter2Values();
    }
    private void Chapter1Values()
    {
        do {
            score = Random.Range(-5, 6);
            foreach (int excludedValue in excludedScores)
            {
                if (score == excludedValue) score = 0;
            }
        } while (score == 0);

        ChooseSprite(score);
        value.text = score.ToString();
    }
    private void Chapter2Values()
    {
        do {
            if (excludedScores != null && excludedScores.First() > 0) score = Random.Range(2,11);
            else if (excludedScores != null && excludedScores.First() < 0) score = Random.Range(-10, -1);
            else score = Random.Range(-10, 11);

            foreach (int excludedValue in excludedScores)
            {
                if (score == excludedValue) score = 0;
            }
        } while ( score < 2 && -2 < score);

        ChooseSprite(score);
        if (score < 0) value.text = "÷" + Math.Abs(score).ToString();
        if (score > 0) value.text = "×" + score.ToString();
    }
    void ChooseSprite(int score)
    {
        spriteRng = Random.Range(0, 3);
        if (score < 0) mySprite.sprite = evilObstacleSprite[spriteRng];
        else mySprite.sprite = goodObstacleSprite[spriteRng];
    }
}
