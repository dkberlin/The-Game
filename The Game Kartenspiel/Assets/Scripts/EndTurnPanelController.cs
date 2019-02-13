using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnPanelController : MonoBehaviour {
	
    public void SetEndTurnButton(bool boolean)
    {
        this.gameObject.SetActive(boolean);
    }
	

}
