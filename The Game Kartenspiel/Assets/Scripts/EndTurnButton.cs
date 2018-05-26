using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheGameNameSpace;
using System;

public class EndTurnButton : MonoBehaviour
{
    public void OnClick()
    {
        //var handCardSlot = FindObjectOfType<CardSlotHandCards>();
        int currentPlayerNumber = GameCore.currentPlayer == 1 ? 1 : 2;
        var currentPlayer = currentPlayerNumber == 1 ? GameCore.player1 : GameCore.player2;

        var handCardSlot = GameCore.GetHandCardSlotOfPlayer(currentPlayer);

        if (handCardSlot != null)
        {
            if (GameCore.numberOfPlayers == 2)
            {

                RefillHandCards(handCardSlot);
                handCardSlot.UpdateHandCards();
                //SaveHandCardsForCurrentPlayer(handCardSlot, currentPlayer);
                //handCardSlot.currentHandCards.Clear();
                //handCardSlot.ClearAllHandCards();

                GameCore.SetCurrentPlayerHandCardsVisible();
                //if (GameCore.LoadHandCardsForPlayer(currentPlayer).Count == 0)
                //{
                //    RefillHandCards(handCardSlot);
                //}
                //else
                //{
                //    GameCore.LoadHandCardsForPlayer(currentPlayer);
                //}
            }
            else
            {
                RefillHandCards(handCardSlot);
                handCardSlot.UpdateHandCards();
            }
        }

        var buttonPanel = FindObjectOfType<EndTurnPanelController>();
        GameCore.cardsDropped = 0;
        buttonPanel.SetEndTurnButton(false);
    }

    //private void SaveHandCardsForCurrentPlayer(CardSlotHandCards handCardSlot, int currentPlayer)
    //{
    //    GameCore.SaveHandCardsForPlayer(currentPlayer, handCardSlot);
    //}

    private void RefillHandCards(CardSlotHandCards handCardSlot)
    {
        int fullHandCardsAmount = GameCore.numberOfHandCards;

        int currentAmountOfHandCards = handCardSlot.GetCardAmountInSlot(handCardSlot.transform);

        var cardHandler = FindObjectOfType<CardHandler>();

        cardHandler.RefillHandCards(fullHandCardsAmount - currentAmountOfHandCards, GameCore.currentPlayer);
    }
	
}
