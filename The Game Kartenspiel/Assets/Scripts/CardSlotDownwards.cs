using UnityEngine.EventSystems;
using TheGameNameSpace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardSlotDownwards : CardSlotBase, IDropHandler
{
    public static int currentSlotNumber = 200;
    public static bool hasCardPlaced = false;
    public EndTurnPanelController endTurnPanelController;

    public void OnDrop(PointerEventData eventData)
    {
        var draggedCard = DragHandler.draggedCard;
        int cardNumber = draggedCard._cardNumber;
        var cardInSlot = _cardInSlot(transform);

        currentSlotNumber = cardInSlot ? cardInSlot._cardNumber : 200;
        hasCardPlaced = cardInSlot ? true : false;

        if (!cardInSlot || cardNumber < currentSlotNumber || currentSlotNumber + 10 == cardNumber)
        {
            if (hasCardPlaced)
            {
                _cardInSlot(transform).gameObject.SetActive(false);
            }
            CardHandler.DisableDragHandler(draggedCard, cardInSlot);
            DragHandler.draggedCard.transform.SetParent(transform);

            var currentPlayerNumber = GameCore.currentPlayer;
            var currentPlayer = currentPlayerNumber == 1 ? GameCore.player1 : GameCore.player2;

            var playersHandCards = GameCore.GetHandCardSlotOfPlayer(currentPlayer);
            playersHandCards.currentHandCards.Remove(draggedCard);
            CheckIfGameWon(playersHandCards.currentHandCards.Count);


            GameCore.cardsDropped++;

            if (GameCore.cardsDropped == 1 && !GameCore.CanStillWinTheGame())
            {
                Debug.Log("Game Over!");
                SceneManager.LoadScene("GameOverScene");
            }

            if (GameCore.cardsDropped >= 2)
            {
                endTurnPanelController.SetEndTurnButton(true);
            }
        }
    }
    public override int GetNumberOfCardInSlot(CardSlotDownwards slot)
    {
        return base.GetNumberOfCardInSlot(slot);
    }
}
