﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TheGameNameSpace
{
    public class GameCore : MonoBehaviour {

        public CardHandler cardHandler;
        public static int numberOfHandCards;
        public static int numberOfPlayers = 1;
        public static List<int> drawnNumbers = Enumerable.Range(2, 98).ToList();
        public int[] savedHandCardsPlayer1;
        public int[] savedHandCardsPlayer2;
        public static int currentPlayer = 1;
        public UnityEngine.Object playerPrefab;
        public List<GameObject> listOfPlayers;
        public static List<GameObject> internalListOfPlayers = new List<GameObject>();

        public static Player player1;
        public static Player player2;

        public static CardSlotHandCards cardSlotPlayer1;
        public static CardSlotHandCards cardSlotPlayer2;
        private static CardSlotHandCards activeHandCardSlot;

        public static int cardsDropped = 0;

        private static List<CardSlot> upwardCards;
        private static List<CardSlot> downwardCards;

        public static ShowCurrentPlayerNumber playerNumberUIElement;

        public static bool lostTheGame = false;

        private void OnDestroy()
        {
            GameEvents.OnCardDroppedInSlot -= OnCardDroppedInSlot;
            GameEvents.OnEndTurnButtonClicked -= SetupNextRound;
        }

        void Start () {
            
            GameEvents.OnCardDroppedInSlot += OnCardDroppedInSlot;
            GameEvents.OnEndTurnButtonClicked += SetupNextRound;
            
            
            for (int i = 0; i < numberOfPlayers; i++)
            {
                InstantiatePlayers(i +1);
            }

            upwardCards = new List<CardSlot>();
            downwardCards = new List<CardSlot>();
            
            var cardSlots = FindObjectsOfType<CardSlot>();

            foreach (var slot in cardSlots)
            {
                if (slot.isUpwardSlot)
                {
                    upwardCards.Add(slot);
                }
                else
                {
                    downwardCards.Add(slot);
                }
            }

            playerNumberUIElement = FindObjectOfType<ShowCurrentPlayerNumber>();


            if (numberOfPlayers == 2)
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

                    cardSlotPlayer1 = cardHandler.cardSlotHandCardsPlayer1;
                    cardSlotPlayer2 = cardHandler.cardSlotHandCardsPlayer2;

                }
            }

            if (player1 == null)
            {
                player1 = FindObjectOfType<Player>();
                cardSlotPlayer1 = cardHandler.cardSlotHandCardsPlayer1;
                activeHandCardSlot = cardSlotPlayer1;
            }

            cardHandler.OnGameStart();
	    }

        private void SetupNextRound()
        {
            if (numberOfPlayers == 2)
            {
                SetPlayerHandCardsNonVisible(currentPlayer);
                
                int nextPlayer = currentPlayer == 1 ? 2: 1;
                
                playerNumberUIElement.SetPlayerNumber(nextPlayer);
                currentPlayer = nextPlayer;
            }
            
            cardsDropped = 0;
        }

        private void OnCardDroppedInSlot(CardBase droppedcard, int numberofcardsinhand)
        {
            CheckIfGameOver(droppedcard, numberofcardsinhand);
        }

        public static CardSlotHandCards GetHandCardSlotOfPlayer(Player player)
        {
            if (player.playerNumber == 1)
            {
                return cardSlotPlayer1;
            }
            else
            {
                return cardSlotPlayer2;
            }
        }

        internal static void SetPlayerHandCardsNonVisible(int playerNumber)
        {
            if (playerNumber == 2)
            {
                cardSlotPlayer2.gameObject.SetActive(false);
                cardSlotPlayer1.gameObject.SetActive(true);
                activeHandCardSlot = cardSlotPlayer1;
            }
            else
            {
                cardSlotPlayer1.gameObject.SetActive(false);
                cardSlotPlayer2.gameObject.SetActive(true);
                activeHandCardSlot = cardSlotPlayer2;
            }
        }

        public void InstantiatePlayers(int playerNumber)
        {
            var newPlayer = Instantiate(playerPrefab) as GameObject;
            
            var playerInfo = newPlayer.GetComponent<Player>();
            playerInfo.playerNumber = playerNumber;

            internalListOfPlayers.Add(newPlayer);
            listOfPlayers.Add(newPlayer);
        }

        public static bool CanStillWinTheGame()
        {
            bool downwardCardToPlace = false;
            bool upwardCardToPlace = false;

            foreach (var card in activeHandCardSlot.currentHandCards)
            {
                foreach (var slot in downwardCards)
                {
                    int slotNumber = slot.GetNumberOfCardInSlot(slot);
                    if (card.CardNumber < slotNumber)
                    {
                        downwardCardToPlace = true;
                    }
                }

                foreach (var slot in upwardCards)
                {
                    int slotnumber = slot.GetNumberOfCardInSlot(slot);
                    if (card.CardNumber > slotnumber)
                    {
                        upwardCardToPlace = true;
                    }
                }
            }

            if (!downwardCardToPlace && !upwardCardToPlace)
            {
                lostTheGame = true;
                return false;
            }

            return true;
        }
        
        public void CheckIfGameOver(CardBase droppedCard, int numberOfCardsInHand)
        {
            int numberOfCardsLeft = drawnNumbers.Count;
            
            //check if game won
            if (numberOfCardsInHand == 0 && numberOfCardsLeft == 0)
            {
                Debug.Log("Game Won!");
                lostTheGame = false;
                FadeItAll.FadeSceneChange("GameOverScene",Color.black, 3f);
            }
            
            // check if game lost
            else if (cardsDropped == 1 && !CanStillWinTheGame())
            {
                Debug.Log("Game Over!");
//                SceneManager.LoadScene("GameOverScene");
                FadeItAll.FadeSceneChange("GameOverScene",Color.black, 3f);
            }
        }
        
        public void LoadMainMenu()
        {
            FadeItAll.FadeSceneChange("MainMenuScene",Color.black, 5f);
        }
    }
}
