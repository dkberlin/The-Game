using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TheGameNameSpace;

public class CardSlotDownwards: CardSlotBase, IDropHandler {

    private static int currentSlotNumber;
    public static bool hasCardPlaced = false;
    public CardSlotHandCards handCardSlot;
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

            //handCardSlot.UpdateHandCards();

            if (GameCore.numberOfHandCards - handCardSlot.GetCardAmountInSlot(handCardSlot.transform) >= 2)
            {
                endTurnPanelController.SetEndTurnButton(true);
            }
        }
    }
}
