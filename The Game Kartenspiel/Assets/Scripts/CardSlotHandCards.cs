using System.Collections.Generic;

public class CardSlotHandCards : CardSlotBase
{
    public List<CardBase> currentHandCards;

    internal void UpdateHandCards()
    {
        currentHandCards.Clear();
        CardBase[] childElements = transform.GetComponentsInChildren<CardBase>();
        
        foreach (var entry in childElements)
        {
            if (entry.GetComponent<CardBase>() != null)
            {
                currentHandCards.Add(entry);
            }
        }
    }
}
