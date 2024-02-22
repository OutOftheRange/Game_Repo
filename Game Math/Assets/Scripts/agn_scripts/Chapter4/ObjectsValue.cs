using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsValue : MonoBehaviour
{
    private int itemValue;

    public void SetItemValue(int newItemValue)
    {
        itemValue = newItemValue;
    }

    public int TakeItemValue()
    {
        return itemValue;
    }
}
