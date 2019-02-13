using System.Collections;
using System.Collections.Generic;
using TheGameNameSpace;
using UnityEngine;
using UnityEngine.UI;

public class WinOrLoseText : MonoBehaviour {

    public Text text;
    public void Start()
    {
        if (GameCore.lostTheGame)
        {
            text.text = "Verloren - Schade!";
        }
        else
        {
            text.text = "Gewonnen!";
        }
    }
}
