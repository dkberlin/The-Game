using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static CardBase draggedCard;
    private Vector3 startPosition;
    private Transform startParent;

    public void OnBeginDrag (PointerEventData eventData)
    {
        draggedCard = gameObject.GetComponent<CardBase>();
        startPosition = transform.position;
        startParent = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        draggedCard = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        if (transform.parent == startParent)
        {
            transform.position = startPosition;
        }
    }
}
