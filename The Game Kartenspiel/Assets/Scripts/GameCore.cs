using System;
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

        private static CardSlotUpwards[] upwardCards;
        private static CardSlotDownwards[] downwardCards;

        public static ShowCurrentPlayerNumber playerNumberUIElement;

        public static bool lostTheGame = false;



        void Start () {
            for (int i = 0; i < numberOfPlayers; i++)
            {
                InstantiatePlayers(i +1);
            }

            upwardCards = FindObjectsOfType<CardSlotUpwards>();
            downwardCards = FindObjectsOfType<CardSlotDownwards>();

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
                    if (card._cardNumber < slotNumber)
                    {
                        downwardCardToPlace = true;
                    }
                }

                foreach (var slot in upwardCards)
                {
                    int slotnumber = slot.GetNumberOfCardInSlot(slot);
                    if (card._cardNumber > slotnumber)
                    {
                        upwardCardToPlace = true;
                    }
                }
            }

            if (!downwardCardToPlace && !upwardCardToPlace)
            {
                return false;
                lostTheGame = true;
            }

            return true;
        }
    }
}
