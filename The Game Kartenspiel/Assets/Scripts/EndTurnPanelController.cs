using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnPanelController : MonoBehaviour {
	[SerializeField] 
	private Animator _animator;
	
    public void SetEndTurnButton(bool boolean)
    {
        this.gameObject.SetActive(boolean);
	    if (boolean)
	    {
			_animator.SetTrigger("FadeIn");
	    }
	    else
	    {
		    _animator.SetTrigger("FadeOut");
	    }
    }
	

}
