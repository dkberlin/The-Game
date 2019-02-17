using UnityEngine;

public class EndTurnPanelController : MonoBehaviour {
	
	private bool _isFadedIn;
	private CanvasGroup _canvas;

	private void Start()
	{
		_canvas = gameObject.GetComponent<CanvasGroup>();
		GameEvents.OnCardDroppedInSlot += OnCardDroppedInSlot;
		
		if (_canvas != null)
		{
			_canvas.alpha = 0f;
		}
	}

	private void OnDestroy()
	{
		GameEvents.OnCardDroppedInSlot -= OnCardDroppedInSlot;
	}

	private void OnCardDroppedInSlot(CardBase droppedCard, int numberOfCardsInHand)
	{
		_canvas.interactable = false;
		_isFadedIn = false;
		FadeItAll.FadeCanvasGroup(_canvas, 3f, false);
	}

	public void SetEndTurnButton(bool boolean)
    {
	    if (!boolean)
	    {
		    FadeItAll.FadeCanvasGroup(_canvas,3,false);
		    _isFadedIn = false;
		    return;
	    }
	    
	    if (_isFadedIn)
	    {
		    return;
	    }
	    
	    FadeItAll.FadeCanvasGroup(_canvas,3,true);
	    _isFadedIn = true;
    }
}