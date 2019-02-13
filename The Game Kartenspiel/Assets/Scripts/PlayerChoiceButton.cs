using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheGameNameSpace;
using UnityEngine.SceneManagement;

public class PlayerChoiceButton : MonoBehaviour {

	public void OnClicked()
    {
        if (transform.name == "1Player")
        {
            GameCore.numberOfHandCards = 8;

//            SceneManager.LoadScene("gameScene");
        }

        else
        {
            GameCore.numberOfHandCards = 7;
            GameCore.numberOfPlayers = 2;

//            SceneManager.LoadScene("gameScene");

        }
    }
}
