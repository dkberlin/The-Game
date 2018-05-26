using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TheGameNameSpace;

public class CardHandler : MonoBehaviour {

    public CardBase playerCard;
    public CardSlotHandCards cardSlotHandCardsPlayer1;
    public CardSlotHandCards cardSlotHandCardsPlayer2;
    private Player player1;
    private Player player2;

    public void OnGameStart()
    {
        if (GameCore.numberOfPlayers == 2)
        {
            player2 = GameCore.player2;
            cardSlotHandCardsPlayer2.gameObject.SetActive(true);
            DrawFirstCards(2);
        }

        player1 = GameCore.player1;
        DrawFirstCards(1);
    }

    public void DrawFirstCards(int playerNumber)
    {
        RefillHandCards(GameCore.numberOfHandCards, playerNumber);
        if (GameCore.numberOfPlayers == 2)
        {
            GameCore.SetPlayerHandCardsNonVisible(2);
        }
    }

    public void RefillHandCards(int cardAmountToRefill, int playerNumber)
    {
        for (int i = 0; i < cardAmountToRefill; i++)
        {
            var card = Instantiate(playerCard);
            int cardNumber = card.GetComponent<CardBase>()._cardNumber;
            var cardTextComponent = card.GetComponentInChildren<Text>();
            var bigCardNumber = card.transform.Find("CardNumber").GetComponent<Text>();
            var smallCardNumber = card.transform.Find("CardNumberSmall").GetComponent<Text>();

            if (playerNumber == 1)
            {
                cardSlotHandCardsPlayer1.currentHandCards.Add(card);
                card.transform.SetParent(cardSlotHandCardsPlayer1.transform);
                player1.savedHandCards.Add(card);
            }
            else
            {
                cardSlotHandCardsPlayer2.currentHandCards.Add(card);
                card.transform.SetParent(cardSlotHandCardsPlayer2.transform);
                player2.savedHandCards.Add(card);
            }
      
            if (bigCardNumber != null && smallCardNumber != null)
            {
                bigCardNumber.text = cardNumber.ToString();
                smallCardNumber.text = cardNumber.ToString();
            }
        }
    }

    internal static void UpdateListOfCards(int randomIndex)
    {
        GameCore.drawnNumbers.RemoveAt(randomIndex);
    }

    public static void DisableDragHandler(CardBase draggedCard, CardBase cardInSlot = null)
    {
        draggedCard.GetComponent<DragHandler>().enabled = false;

        if (cardInSlot != null)
        {
            cardInSlot.GetComponent<DragHandler>().enabled = false;
        }
    }
}
