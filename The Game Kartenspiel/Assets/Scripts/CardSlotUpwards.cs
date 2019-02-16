﻿using UnityEngine.EventSystems;
using TheGameNameSpace;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CardSlotUpwards : CardSlotBase, IDropHandler
{
    public static int currentSlotNumber;
    public static bool hasCardPlaced;
    public EndTurnPanelController endTurnPanelController;
    public Button validationButton;
    public Button declineButton;
    
    private CardBase _currentDraggedCard;
    private CardBase _cardInPlace;
    private CardSlotHandCards _currentPlayerHandCards;

    public void OnDrop(PointerEventData eventData)
    {
        _currentDraggedCard = DragHandler.draggedCard;
        int cardNumber = _currentDraggedCard._cardNumber;
        _cardInPlace = _cardInSlot(transform);

        currentSlotNumber = _cardInPlace ? _cardInPlace._cardNumber : 200;
        hasCardPlaced = _cardInPlace;

        if (!_cardInPlace || cardNumber > currentSlotNumber || currentSlotNumber - 10 == cardNumber)
        {
            _currentDraggedCard.transform.SetParent(transform);
            _currentDraggedCard.transform.localPosition = new Vector3(0,0,0);
            
            SetChoiceButtons(true);
            CardHandler.DisableDragHandler(_currentDraggedCard, _cardInPlace);
            

            var currentPlayerNumber = GameCore.currentPlayer;
            var currentPlayer = currentPlayerNumber == 1 ? GameCore.player1 : GameCore.player2;

            _currentPlayerHandCards = GameCore.GetHandCardSlotOfPlayer(currentPlayer);
            _currentPlayerHandCards.currentHandCards.Remove(_currentDraggedCard);
            CheckIfGameWon(_currentPlayerHandCards.currentHandCards.Count);

            if (GameCore.cardsDropped == 1 && !GameCore.CanStillWinTheGame())
            {
                Debug.Log("Game Over!");
                SceneManager.LoadScene("GameOverScene");
            }
        }
    }

    private void SetChoiceButtons(bool value)
    {
        validationButton.transform.gameObject.SetActive(true);
        declineButton.transform.gameObject.SetActive(true);
        
        if (value)
        {
            FadeItAll.FadeCanvasGroup(validationButton.GetComponent<CanvasGroup>(), 3, true);
            FadeItAll.FadeCanvasGroup(declineButton.GetComponent<CanvasGroup>(), 3, true);
            validationButton.onClick.AddListener(OnValidationClicked);
            declineButton.onClick.AddListener(OnDeclineClicked);
        }
        else
        {
            FadeItAll.FadeCanvasGroup(validationButton.GetComponent<CanvasGroup>(), 3, false);
            FadeItAll.FadeCanvasGroup(declineButton.GetComponent<CanvasGroup>(), 3, false);
            validationButton.onClick.RemoveListener(OnValidationClicked);
            declineButton.onClick.RemoveListener(OnDeclineClicked);
        }
    }

    private void OnDeclineClicked()
    {
        CardHandler.EnableDragHandler(_currentDraggedCard, _cardInPlace);
        DragHandler.draggedCard.transform.SetParent(_currentPlayerHandCards.transform);
        SetChoiceButtons(false);
    }

    private void OnValidationClicked()
    {
        if (hasCardPlaced)
        {
            _cardInPlace.transform.gameObject.SetActive(false);
        }
        
        GameCore.cardsDropped++;
        SetChoiceButtons(false);
        
        if (GameCore.cardsDropped >= 2)
        {
            endTurnPanelController.SetEndTurnButton(true);
        }
    }
}
