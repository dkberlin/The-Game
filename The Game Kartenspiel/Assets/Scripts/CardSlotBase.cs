using System.Collections;
using System.Collections.Generic;
using TheGameNameSpace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardSlotBase : MonoBehaviour
{
    public bool isUpwardSlot;

    private static int _numberOfCardsInSlot;

    public virtual int GetCardAmountInSlot(Transform givenTransform)
    {
        _numberOfCardsInSlot = givenTransform.childCount;
        return _numberOfCardsInSlot;
    }

    public CardBase _cardInSlot(Transform givenTransform)
    {
        _numberOfCardsInSlot = GetCardAmountInSlot(givenTransform);
        if (_numberOfCardsInSlot > 0)
        {
            CardBase cardInSlot = transform.GetChild(_numberOfCardsInSlot-1).gameObject.GetComponent<CardBase>();
            return cardInSlot;
        }
        
        return null;
    }

    public int GetNumberOfCardInSlot(CardSlot slot)
    {
        var cardInSlot = _cardInSlot(slot.transform);
        
        if (cardInSlot == null)
        {
            if (!slot.isUpwardSlot)
            {
                return 101;
            }
            
            else if (slot.isUpwardSlot)
            {
                return 0;
            }
        }
        
        return cardInSlot.CardNumber;
    }

    
}
