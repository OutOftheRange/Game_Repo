using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using UnityEditor;

public class NumberMarkLogic : MonoBehaviour
{
    private SpriteRenderer displaySprite;
    private SpriteRenderer selectionSprite;
    [SerializeField] private Sprite questionMarkSprite;
    [SerializeField] private Sprite xMarkSprite;
    [SerializeField] private Sprite[] evilTargetSprite;
    [SerializeField] private Sprite[] goodTargetSprite;
    [SerializeField] private Animator animeLine;
    private TextMeshPro valueDisplay;
    public bool isInvisible = false;
    public int value;

    void Start()
    {
        displaySprite = gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>();
        selectionSprite = gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>();
        valueDisplay = gameObject.transform.GetChild(0).GetComponent<TextMeshPro>();
        animeLine = gameObject.GetComponentInParent<Animator>();
        GenerateMark();
    }

    public void GenerateMark()
    {
        int spriteRng = Random.Range(0,3);
        if (isInvisible) 
        {
            valueDisplay.text = "?";
            displaySprite.sprite = questionMarkSprite;
        }
        else 
        {
            valueDisplay.text = value.ToString();
            displaySprite.sprite = goodTargetSprite[spriteRng];
        }
    }

    void OnMouseDown()
    {
        if (displaySprite.sprite == questionMarkSprite){
            int spriteRng = Random.Range(0,3);
            if (value == NumberLineGenerator.Instance.GetTarget()){               
                displaySprite.sprite = evilTargetSprite[spriteRng];
                valueDisplay.text = value.ToString();
                NumberLineGenerator.Instance.EnableAnimation();
                animeLine.SetTrigger("TargetHit");
            }
            else{
                displaySprite.sprite = goodTargetSprite[spriteRng];
                valueDisplay.text = value.ToString();
                HPLogic.Instance.TakeDamage();
            }
        } 
    }

    void OnMouseOver()
    {
        selectionSprite.sprite = xMarkSprite;
    }
    void OnMouseExit()
    {
        selectionSprite.sprite = null;
    }    
}
