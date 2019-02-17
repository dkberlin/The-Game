using UnityEngine;
using TheGameNameSpace;

public class EndTurnButton : MonoBehaviour
{
    [SerializeField]
    private  EndTurnPanelController _buttonPanel;
    
    public void OnClick()
    {
        GameEvents.Involve_OnEndTurnButtonClicked();

        _buttonPanel.SetEndTurnButton(false);
        
        if (!GameCore.CanStillWinTheGame())
        {
            Debug.Log("Game Over!");
            FadeItAll.FadeSceneChange("GameOverScene", Color.black, 3f);
        }
    }
}
