using UnityEngine;

/// <summary>
/// Basic exit button functionality. In the game it will return to the menu, and in the menu it will exit to desktop.
/// </summary>

[RequireComponent(typeof(UIButton))]
public class UIExitButton : MonoBehaviour
{
#if UNITY_WEBPLAYER
	void Start ()
	{
		UIButton btn = GetComponent<UIButton>();
		if (btn != null && Application.loadedLevelName == "POI") btn.isEnabled = false;
	}
#endif

	void OnClick ()
	{
#if UNITY_4_7
		if (Application.loadedLevelName == "POI")
#else
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "POI")
#endif
		{
			Application.Quit();
		}
		else
		{
			GameManager.Forfeit();
		}
	}
}
