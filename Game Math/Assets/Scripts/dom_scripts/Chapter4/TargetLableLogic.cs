using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetLableLogic : MonoBehaviour
{
    public static TargetLableLogic Instance { get {return _instance;}}
    private static TargetLableLogic _instance;
    private int targetValue;
    private TextMeshPro valueDisplay;

    void Awake()
    {
        valueDisplay = gameObject.transform.GetChild(0).GetComponent<TextMeshPro>();
        _instance = this;
    }

    public void WriteTarget(int targetValue)
    {
        valueDisplay.text = "Cel:\n" + targetValue.ToString();
    }
}
