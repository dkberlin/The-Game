using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheGameNameSpace;
using System;
using UnityEngine.SceneManagement;

public class EndTurnButton : MonoBehaviour
{
    public void OnClick()
    {
        int currentPlayerNumber = GameCore.currentPlayer == 1 ? 1 : 2;
        var currentPlayer = currentPlayerNumber == 1 ? GameCore.player1 : GameCore.player2;
        int nextPlayer = currentPlayerNumber == 1 ? 2 : 1;


        var handCardSlot = GameCore.GetHandCardSlotOfPlayer(currentPlayer);

        if (handCardSlot != null)
        {
            if (GameCore.numberOfPlayers == 2)
            {
                RefillHandCards(handCardSlot);
                handCardSlot.UpdateHandCards();

                GameCore.SetPlayerHandCardsNonVisible(currentPlayerNumber);
            }
            else
            {
                RefillHandCards(handCardSlot);
                handCardSlot.UpdateHandCards();
            }
        }

        var buttonPanel = FindObjectOfType<EndTurnPanelController>();
        GameCore.cardsDropped = 0;

        if (GameCore.numberOfPlayers == 2)
        {
            GameCore.playerNumberUIElement.SetPlayerNumber(nextPlayer);
            GameCore.currentPlayer = nextPlayer;
        }

        buttonPanel.SetEndTurnButton(false);
        if (!GameCore.CanStillWinTheGame())
        {
            Debug.Log("Game Over!");
            SceneManager.LoadScene("GameOverScene");
        }
    }

    private void RefillHandCards(CardSlotHandCards handCardSlot)
    {
        int fullHandCardsAmount = GameCore.numberOfHandCards;

        int currentAmountOfHandCards = handCardSlot.GetCardAmountInSlot(handCardSlot.transform);

        var cardHandler = FindObjectOfType<CardHandler>();

        cardHandler.RefillHandCards(fullHandCardsAmount - currentAmountOfHandCards, GameCore.currentPlayer);
    }
	
}
