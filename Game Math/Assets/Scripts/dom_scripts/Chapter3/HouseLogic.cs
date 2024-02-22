using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class HouseLogic : MonoBehaviour, IDropHandler
{
    private TextMeshProUGUI houseValueDisplay;
    private string calculationValue;
    public static int totalScore;
    [SerializeField] private Animator animeHouse;
    [SerializeField] private Animator animeRoman;

    private void Awake(){
        houseValueDisplay = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        houseValueDisplay.text = "";
    }

    public void OnDrop (PointerEventData eventData){
        if (eventData.pointerDrag != null){
            calculationValue += eventData.pointerDrag.GetComponent<ColumnTablet>().ReadColumnValue();
            ValidateScore(GetComponent<RomanNumbersConverter>().RomanToInt(calculationValue));
            houseValueDisplay.text = calculationValue;
            eventData.pointerDrag.GetComponent<ColumnTablet>().ResetPosition();
        }
    }
    private void ValidateScore(int score)
    {
        if (score == HouseRequestGenerator.Instance.houseValue){
            totalScore += score/5;
            StartCoroutine(NewRequestTransition());
        }
        if (score == 0){
            calculationValue = null;
        }
    }

    IEnumerator NewRequestTransition()
    {
        animeRoman.SetTrigger("HouseComplete");
        yield return new WaitForSecondsRealtime(1f);
        animeHouse.SetTrigger("NewHouse");
        yield return new WaitForSecondsRealtime(0.7f);
        houseValueDisplay.text = null;
        calculationValue = null;
        HouseRequestGenerator.Instance.GenerateRequest();
        animeRoman.SetTrigger("NewNPC");
    }

}
