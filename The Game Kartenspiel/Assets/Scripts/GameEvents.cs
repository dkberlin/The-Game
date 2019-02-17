public static class GameEvents  
{
	public delegate void CardDroppedInSlot(CardBase droppedCard, int numberOfCardsInHand);
	public static event CardDroppedInSlot OnCardDroppedInSlot;
	
	public delegate void CardDropDeclined();
	public static event CardDropDeclined OnCardDropDeclined;
	
	public delegate void EndTurnButtonClicked();
	public static event EndTurnButtonClicked OnEndTurnButtonClicked;
	
	public static void Invoke_OnCardDroppedInSlot(CardBase droppedCard, int numberOfCardsInHand)
	{
		if (OnCardDroppedInSlot != null)
		{
			OnCardDroppedInSlot(droppedCard, numberOfCardsInHand);
		}
	}

	public static void Invoke_OnCardDropDeclined()
	{
		if (OnCardDropDeclined != null)
		{
			OnCardDropDeclined();
		}
	}
	
	public static void Involve_OnEndTurnButtonClicked()
	{
		if (OnEndTurnButtonClicked != null)
		{
			OnEndTurnButtonClicked();
		}
	}
}


