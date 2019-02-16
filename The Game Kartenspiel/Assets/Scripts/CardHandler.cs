﻿using System;
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
            var bigCardNumber = card.bigNumber;
            var smallCardNumber = card.smallNumber;

            if (playerNumber == 1)
            {
                cardSlotHandCardsPlayer1.currentHandCards.Add(card);
                card.transform.SetParent(cardSlotHandCardsPlayer1.transform);
                card.transform.localScale = new Vector3(1,1,1);
                player1.savedHandCards.Add(card);
            }
            else
            {
                cardSlotHandCardsPlayer2.currentHandCards.Add(card);
                card.transform.SetParent(cardSlotHandCardsPlayer2.transform);
                card.transform.localScale = new Vector3(1,1,1);
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
    
    public static void EnableDragHandler(CardBase draggedCard, CardBase cardInSlot = null)
    {
        draggedCard.GetComponent<DragHandler>().enabled = true;
        draggedCard.GetComponent<CanvasGroup>().blocksRaycasts = true;

        if (cardInSlot != null)
        {
            cardInSlot.GetComponent<DragHandler>().enabled = true;
        }
    }
    
    public void EnableAllHandCards(bool shouldbeEnabled)
    {
        foreach (var card in cardSlotHandCardsPlayer1.currentHandCards)
        {
            card.GetComponent<DragHandler>().enabled = shouldbeEnabled;
        }

        foreach (var card in cardSlotHandCardsPlayer2.currentHandCards)
        {
            card.GetComponent<DragHandler>().enabled = shouldbeEnabled;
        }
    }
}
