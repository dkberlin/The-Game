using UnityEngine;
using TheGameNameSpace;

public class PlayerChoiceButton : MonoBehaviour 
{
    public AudioSource backGroundMusicSource;

	public void OnClicked()
    {
        FadeItAll.FadeAudio(backGroundMusicSource, 1, false);
        
        if (transform.name == "1Player")
        {
            GameCore.numberOfHandCards = 8;
            FadeItAll.FadeSceneChange("gameScene",Color.black, 1f);
        }

        else
        {
            GameCore.numberOfHandCards = 7;
            GameCore.numberOfPlayers = 2;
            
            FadeItAll.FadeSceneChange("gameScene",Color.black, 1f);
        }
    }
}
