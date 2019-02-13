using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TheGameNameSpace;
using TMPro;

public class CardBase : MonoBehaviour
{
    public TMP_Text bigNumber;
    public TMP_Text smallNumber;
    
    private int cardNumber = 0;
    public int _cardNumber
    {
        get
        {
            if (cardNumber == 0)
            {
                return SetRandomCardNumber();
            }
            else
            {
                return cardNumber;
            }
        }
    }

    private int SetRandomCardNumber()
    {
        int randomIndex = UnityEngine.Random.Range(0, (int)GameCore.drawnNumbers.Count -1);
        int drawnNumber = GameCore.drawnNumbers[randomIndex];

        CardHandler.UpdateListOfCards(randomIndex);

        cardNumber = drawnNumber;
        return drawnNumber;
    }
}
