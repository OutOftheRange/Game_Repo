using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnSpawner : MonoBehaviour
{
    [SerializeField] private GameObject column;
    private ColumnTablet newColumn;
    private int offsetY = 0;
    private int offsetX = -300;
    private int uiLength = 700;
    private int columnAmount = 7;

    void Start()
    {
        SpawnColumnSet();
    }

    private void SpawnColumnSet(){
        for (int i = 0; i <columnAmount; i++){
            GameObject columnInstance = Instantiate(column, new Vector2(offsetX, offsetY), transform.rotation);
            columnInstance.transform.SetParent(gameObject.transform, false);
            
            columnInstance.GetComponent<ColumnTablet>().ColumnTabletValueSetter(i);

            offsetX += uiLength/columnAmount;
        }
    }
}
