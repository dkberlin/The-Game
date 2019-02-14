using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class MenuItems
{
	[MenuItem("Tools/Play From Main Menu")]
	private static void NewMenuOption()
	{
		EditorSceneManager.OpenScene("MainMenuScene");
	}
}