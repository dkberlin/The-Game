using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TheGameNameSpace;

public class CardHandler : MonoBehaviour {

    //public UnityEngine.Object playerCard;
    public CardBase playerCard;
    //public List<UnityEngine.Object> playerHandCards;
    //public Transform handCardPanel;
    public CardSlotHandCards cardSlotHandCardsPlayer1;
    public CardSlotHandCards cardSlotHandCardsPlayer2;
    private Player player1;
    private Player player2;

    public void OnGameStart()
    {
        //int usedHandCards = GameCore.numberOfHandCards;

        //RefillHandCards(usedHandCards);
        if (GameCore.numberOfPlayers == 2)
        {
            var players = FindObjectsOfType<Player>();
            foreach (var player in players)
            {
                if (player.playerNumber == 2)
                {
                    player2 = player;
                }

                if (player.playerNumber == 1)
                {
                    player1 = player;
                }
            }
            cardSlotHandCardsPlayer2.gameObject.SetActive(true);
            DrawFirstCards(2);
        }
        
        if (player1 == null)
        {
            player1 = FindObjectOfType<Player>();
        }

        DrawFirstCards(1);
    }

    public void DrawFirstCards(int playerNumber)
    {
        RefillHandCards(GameCore.numberOfHandCards, playerNumber);
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

            //playerHandCards[i] = card;
            if (playerNumber == 1)
            {
                cardSlotHandCardsPlayer1.currentHandCards.Add(card);
                card.transform.SetParent(cardSlotHandCardsPlayer1.transform);   
                 
            }
            else
            {
                cardSlotHandCardsPlayer2.currentHandCards.Add(card);
                card.transform.SetParent(cardSlotHandCardsPlayer2.transform);
            }
            //cardSlotHandCards.currentHandCards.Add(card);
      
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
