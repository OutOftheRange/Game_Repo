using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ColumnLogic : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform itemPosition;
    private CanvasGroup canvasGroup;

    private void Awake(){
        itemPosition = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GameObject.FindWithTag("Canvas").GetComponent<Canvas>();
    }

    public void OnBeginDrag (PointerEventData eventData){
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag (PointerEventData eventData){
        itemPosition.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag (PointerEventData eventData){
        canvasGroup.blocksRaycasts = true;
        GetComponent<ColumnTablet>().ResetPosition();
    }
}
