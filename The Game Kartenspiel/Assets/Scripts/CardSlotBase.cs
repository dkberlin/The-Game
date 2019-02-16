using System.Collections;
using System.Collections.Generic;
using TheGameNameSpace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardSlotBase : MonoBehaviour
{
    [SerializeField] public bool isUpwardSlot;
    
    private static int numberOfCardsInSlot;

    public virtual int GetCardAmountInSlot(Transform givenTransform)
    {
        numberOfCardsInSlot = givenTransform.childCount;
        return numberOfCardsInSlot;
    }

    public CardBase _cardInSlot(Transform givenTransform)
    {
        numberOfCardsInSlot = GetCardAmountInSlot(givenTransform);
        if (numberOfCardsInSlot > 0)
        {
            CardBase cardInSlot = transform.GetChild(numberOfCardsInSlot-1).gameObject.GetComponent<CardBase>();
            return cardInSlot;
        }
        return null;
    }

    public virtual int GetNumberOfCardInSlot(CardSlot slot)
    {
        var cardInSlot = _cardInSlot(slot.transform);

        if (cardInSlot == null)
        {
            return 101;
        }

        return cardInSlot._cardNumber;
    }

    public virtual int GetNumberOfCardInSlot(CardSlotUpwards slot)
    {
        var cardInSlot = _cardInSlot(slot.transform);

        if (cardInSlot == null)
        {
            return 0;
        }

        return cardInSlot._cardNumber;
    }

    public virtual void CheckIfGameWon(int cardsInHand)
    {
        int numberOfCardsLeft = GameCore.drawnNumbers.Count;

        if (cardsInHand == 0 && numberOfCardsLeft == 0)
        {
            GameCore.lostTheGame = false;
            SceneManager.LoadScene("GameOverScene");
        }
    }
}
