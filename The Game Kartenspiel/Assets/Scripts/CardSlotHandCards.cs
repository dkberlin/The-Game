using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheGameNameSpace;

public class CardSlotHandCards : CardSlotBase
{
    public List<CardBase> currentHandCards;

    public override int GetCardAmountInSlot(Transform givenTransform)
    {
        return base.GetCardAmountInSlot(givenTransform);
    }

    internal void UpdateHandCards()
    {
        currentHandCards.Clear();
        CardBase[] childElements = transform.GetComponentsInChildren<CardBase>();
        
        foreach (var entry in childElements)
        {
            if (entry.GetComponent<CardBase>() != null)
            {
                currentHandCards.Add(entry);
            }
        }
    }
}
