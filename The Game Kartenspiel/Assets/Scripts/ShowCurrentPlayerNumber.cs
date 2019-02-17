using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowCurrentPlayerNumber : MonoBehaviour {

    public TMP_Text currentText;
	// Use this for initialization
	void Start () {
        currentText.text = "Player 1";
	}

    internal void SetPlayerNumber(int currentPlayerNumber)
    {
        currentText.text = "Player " + currentPlayerNumber;
    }
}
