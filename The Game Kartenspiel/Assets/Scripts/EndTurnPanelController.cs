using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnPanelController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.gameObject.SetActive(false);
	}
	
    public void SetEndTurnButton(bool boolean)
    {
        this.gameObject.SetActive(boolean);
    }
	

}
