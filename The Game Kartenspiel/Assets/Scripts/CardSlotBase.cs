using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlotBase : MonoBehaviour
{
    private static int numberOfCardsInSlot;

    public virtual int GetCardAmountInSlot(Transform givenTransform)
    {
        numberOfCardsInSlot = givenTransform.childCount;
        return numberOfCardsInSlot;
    }

    public CardBase _cardInSlot(Transform givenTransform)
    {
        //int numbersOfCardsInSlot = GetCardAmountInSlot(givenTransform);
        if (numberOfCardsInSlot > 0)
        {
            CardBase cardInSlot = transform.GetChild(numberOfCardsInSlot-1).gameObject.GetComponent<CardBase>();
            return cardInSlot;
        }
        return null;
    }

}
