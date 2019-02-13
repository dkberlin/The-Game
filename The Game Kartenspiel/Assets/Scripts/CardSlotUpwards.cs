using UnityEngine.EventSystems;
using TheGameNameSpace;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CardSlotUpwards : CardSlotBase, IDropHandler
{
    public static int currentSlotNumber = 0;
    public static bool hasCardPlaced = false;
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
//        var cardInSlot = _cardInSlot(transform);
        _cardInPlace = _cardInSlot(transform);

        currentSlotNumber = _cardInPlace ? _cardInPlace._cardNumber : 200;
        hasCardPlaced = _cardInPlace ? true : false;

        if (!_cardInPlace || cardNumber > currentSlotNumber || currentSlotNumber - 10 == cardNumber)
        {
            ActivateChoiceButtons();
//            if (hasCardPlaced)
//            {
//                _cardInSlot(transform).gameObject.SetActive(false);
//            }
            CardHandler.DisableDragHandler(_currentDraggedCard, _cardInPlace);
            _currentDraggedCard.transform.SetParent(transform);
            _currentDraggedCard.transform.localPosition = new Vector3(0,0,0);
            

            var currentPlayerNumber = GameCore.currentPlayer;
            var currentPlayer = currentPlayerNumber == 1 ? GameCore.player1 : GameCore.player2;

            _currentPlayerHandCards = GameCore.GetHandCardSlotOfPlayer(currentPlayer);
            _currentPlayerHandCards.currentHandCards.Remove(_currentDraggedCard);
            CheckIfGameWon(_currentPlayerHandCards.currentHandCards.Count);

//            GameCore.cardsDropped++;

            if (GameCore.cardsDropped == 1 && !GameCore.CanStillWinTheGame())
            {
                Debug.Log("Game Over!");
                SceneManager.LoadScene("GameOverScene");
            }

//            if (GameCore.cardsDropped >= 2)
//            {
//                endTurnPanelController.SetEndTurnButton(true);
//            }
        }
    }

    private void ActivateChoiceButtons()
    {
        validationButton.transform.gameObject.SetActive(true);
        validationButton.onClick.AddListener(OnValidationClicked);
        declineButton.transform.gameObject.SetActive(true);
        declineButton.onClick.AddListener(OnDeclineClicked);
    }
    
    private void DeactivateChoiceButtons()
    {
        validationButton.transform.gameObject.SetActive(false);
        validationButton.onClick.RemoveListener(OnValidationClicked);
        declineButton.transform.gameObject.SetActive(false);
        declineButton.onClick.RemoveListener(OnDeclineClicked);
    }

    private void OnDeclineClicked()
    {
        CardHandler.EnableDragHandler(_currentDraggedCard, _cardInPlace);
        DragHandler.draggedCard.transform.SetParent(_currentPlayerHandCards.transform);
        DeactivateChoiceButtons();
    }

    private void OnValidationClicked()
    {
        if (hasCardPlaced)
        {
            _cardInPlace.transform.gameObject.SetActive(false);
        }
        
        GameCore.cardsDropped++;
        DeactivateChoiceButtons();
        
        if (GameCore.cardsDropped >= 2)
        {
            endTurnPanelController.SetEndTurnButton(true);
        }
    }

    public override int GetNumberOfCardInSlot(CardSlotUpwards slot)
    {
        return base.GetNumberOfCardInSlot(slot);
    }
    
}
