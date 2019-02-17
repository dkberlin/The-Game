using UnityEngine;
using TheGameNameSpace;
using TMPro;

public class CardBase : MonoBehaviour
{
    public TMP_Text bigNumber;
    public TMP_Text smallNumber;
    
    private int _cardNumber;
    public int CardNumber
    {
        get
        {
            if (_cardNumber == 0)
            {
                return SetRandomCardNumber();
            }
            else
            {
                return _cardNumber;
            }
        }
    }

    private int SetRandomCardNumber()
    {
        int randomIndex = UnityEngine.Random.Range(0, (int)GameCore.drawnNumbers.Count -1);
        int drawnNumber = GameCore.drawnNumbers[randomIndex];

        CardHandler.UpdateListOfCards(randomIndex);

        _cardNumber = drawnNumber;
        return drawnNumber;
    }
}
