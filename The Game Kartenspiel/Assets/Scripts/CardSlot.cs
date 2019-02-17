using UnityEngine.EventSystems;
using TheGameNameSpace;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CardSlot : CardSlotBase, IDropHandler
{
    private static int _currentSlotNumber = 200;
    private static bool _hasCardPlaced;
    private CardBase _currentDraggedCard;
    private CardBase _cardInPlace;
    private CardSlotHandCards _currentPlayerHandCards;
    
    public EndTurnPanelController endTurnPanelController;
    public Button validationButton;
    public Button declineButton;
    public CardHandler cardHandler;

    public void OnDrop(PointerEventData eventData)
    {
        _currentDraggedCard = DragHandler.draggedCard;
        var currentPlayer = GameCore.currentPlayer == 1 ? GameCore.player1 : GameCore.player2;

        _currentPlayerHandCards = GameCore.GetHandCardSlotOfPlayer(currentPlayer);
        int cardNumber = _currentDraggedCard.CardNumber;
        _cardInPlace = _cardInSlot(transform);

        _currentSlotNumber = _cardInPlace ? _cardInPlace.CardNumber : 200;
        _hasCardPlaced = _cardInPlace;
        
        if (CanDropCard(cardNumber))
        {
            _currentDraggedCard.transform.SetParent(transform);
            _currentDraggedCard.transform.localPosition = new Vector3(0,0,0);
            
            SetChoiceButtons(true);
            cardHandler.DisableDragHandler(_currentDraggedCard, _cardInPlace);
            
            GameEvents.Invoke_OnCardDroppedInSlot(_currentDraggedCard, _currentPlayerHandCards.currentHandCards.Count);
        }
        
        else
        {
            GameEvents.Invoke_OnCardDropDeclined();
        }
    }
    
    private bool CanDropCard(int cardNumber)
    {
        if (isUpwardSlot)
        {
            return (!_cardInPlace || cardNumber > _currentSlotNumber || _currentSlotNumber - 10 == cardNumber);
        }
        
        return (!_cardInPlace || cardNumber < _currentSlotNumber || _currentSlotNumber + 10 == cardNumber);
    }
    
    private void SetChoiceButtons(bool value)
    {
        cardHandler.EnableAllHandCards(false);
        
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
        cardHandler.EnableAllHandCards(true);

        CardHandler.EnableDragHandler(_currentDraggedCard, _cardInPlace);
        DragHandler.draggedCard.transform.SetParent(_currentPlayerHandCards.transform);
        SetChoiceButtons(false);
    }

    private void OnValidationClicked()
    {
        if (_hasCardPlaced)
        {
            _cardInPlace.transform.gameObject.SetActive(false);
        }
        
        GameCore.cardsDropped++;
        SetChoiceButtons(false);
        
        if (GameCore.cardsDropped >= 2)
        {
            endTurnPanelController.SetEndTurnButton(true);
        }
        
        cardHandler.EnableAllHandCards(true);
    }
}
