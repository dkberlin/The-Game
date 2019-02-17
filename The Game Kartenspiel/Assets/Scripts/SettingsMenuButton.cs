using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingsMenuButton : MonoBehaviour {

	private Button _button;
	public Animator menuAnimator;
	public SettingsMenu settingsMenu;
	
	// Use this for initialization
	void Start () 
	{
		_button = GetComponent<Button>();
		_button.onClick.AddListener(MenuButtonClicked);
	}

	private void MenuButtonClicked()
	{
		if (!settingsMenu.IsShown)
		{
			menuAnimator.ResetTrigger("HideMenu");
			menuAnimator.SetTrigger("ShowMenu");
			settingsMenu.IsShown = true;
		}
		else
		{
			menuAnimator.ResetTrigger("ShowMenu");
			menuAnimator.SetTrigger("HideMenu");
			settingsMenu.IsShown = false;
		}
	}
}
