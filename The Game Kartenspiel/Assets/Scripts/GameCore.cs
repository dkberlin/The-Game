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

        public static int cardsDropped = 0;


        void Start () {
            for (int i = 0; i < numberOfPlayers; i++)
            {
                InstantiatePlayers(i +1);
            }

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

                    cardSlotPlayer2 = cardHandler.cardSlotHandCardsPlayer2;

                }
            }

            if (player1 == null)
            {
                player1 = FindObjectOfType<Player>();
            }

            cardSlotPlayer1 = cardHandler.cardSlotHandCardsPlayer1;

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

        internal static void SetCurrentPlayerHandCardsVisible()
        {
            var playerToSwitchOffHandCards = currentPlayer == 1 ? player2 : player1;
            
            if (playerToSwitchOffHandCards.playerNumber == 2)
            {
                cardSlotPlayer2.gameObject.SetActive(false);
                cardSlotPlayer1.gameObject.SetActive(true);
            }
            else
            {
                cardSlotPlayer1.gameObject.SetActive(false);
                cardSlotPlayer2.gameObject.SetActive(true);
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

        //public static void SaveHandCardsForPlayer(int currentPlayer, CardSlotHandCards handCardSlot)
        //{
        //    //foreach (var player in internalListOfPlayers)
        //    //{
        //    //    var playerInfo = player.GetComponent<Player>();

        //    //    if (playerInfo.playerNumber == currentPlayer)
        //    //    {
        //    //        foreach(var card in handCardSlot.currentHandCards)
        //    //        {
        //    //            playerInfo.savedHandCards.Add(card.GetComponent<CardBase>());
        //    //        }
        //    //    }
        //    //}

        //    var playerToUpdate = currentPlayer == 1 ? player1 : player2;

        //    playerToUpdate.savedHandCards.Clear();

        //    foreach (var card in handCardSlot.currentHandCards)
        //    {
        //        playerToUpdate.savedHandCards.Add(card);
        //    }
        //}

        //public static List<CardBase> LoadHandCardsForPlayer(int currentPlayer)
        //{
        //    var cardList = internalListOfPlayers[currentPlayer].GetComponent<Player>().savedHandCards;

        //    return cardList;
        //}

        public static void CheckGameConditions()
        {
            Debug.Log(player1.savedHandCards[0]._cardNumber);
        }
    }
}
