using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ColumnTablet : MonoBehaviour
{
    private Vector2 originPoint;
    [SerializeField] private TextMeshProUGUI numeral;
    [SerializeField] private string columnValue;

    public void Start (){
        originPoint = GetComponent<RectTransform>().anchoredPosition;
        numeral.text = ReadColumnValue();
    }

    public void ColumnTabletValueSetter(int value){
        string[] possibleValues = {"I", "V", "X", "L", "C", "D", "M"};
        columnValue = possibleValues[value];
    }
    public void ResetPosition(){
        GetComponent<RectTransform>().anchoredPosition = originPoint;
    }
    public string ReadColumnValue(){
        return columnValue;
    }
}
