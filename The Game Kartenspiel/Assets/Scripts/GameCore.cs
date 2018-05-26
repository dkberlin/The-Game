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

        private CardSlotHandCards playerOneHandCards;
        private CardSlotHandCards playerTwoHandCards;


        void Start () {
            for (int i = 0; i < numberOfPlayers; i++)
            {
                InstantiatePlayers(i +1);
            }

            playerOneHandCards = new CardSlotHandCards();
            
            if (numberOfHandCards == 2)
            {
                playerTwoHandCards = new CardSlotHandCards();
            }

            cardHandler.OnGameStart();
	    }

        public void InstantiatePlayers(int playerNumber)
        {
            var newPlayer = Instantiate(playerPrefab) as GameObject;
            
            var playerInfo = newPlayer.GetComponent<Player>();
            playerInfo.playerNumber = playerNumber;

            internalListOfPlayers.Add(newPlayer);
            listOfPlayers.Add(newPlayer);
        }

        public static void SaveHandCardsForPlayer(int currentPlayer, CardSlotHandCards handCardSlot)
        {
            foreach (var player in internalListOfPlayers)
            {
                var playerInfo = player.GetComponent<Player>();

                if (playerInfo.playerNumber == currentPlayer)
                {
                    foreach(var card in handCardSlot.currentHandCards)
                    {
                        playerInfo.savedHandCards.Add(card.GetComponent<CardBase>());
                    }
                }
            }

        }

        public static List<CardBase> LoadHandCardsForPlayer(int currentPlayer)
        {
            var cardList = internalListOfPlayers[currentPlayer].GetComponent<Player>().savedHandCards;

            return cardList;
        }

        public void CheckGameConditions()
        {
            //CardSlotHandCards.
        }
    }
}
