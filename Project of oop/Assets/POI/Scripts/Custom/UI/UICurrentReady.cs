using UnityEngine;

/// <summary>
/// Checkbox controller for the "ready" state.
/// </summary>

[RequireComponent(typeof(UIToggle))]
public class UICurrentReady : MonoBehaviour
{
	UIToggle mCheck;

	void Awake ()
	{
		mCheck = GetComponent<UIToggle>();
		EventDelegate.Add(mCheck.onChange, SaveState);
	}

	void OnEnable () { mCheck.value = PlayerProfile.powerSavingMode; }
	void SaveState () { PlayerProfile.powerSavingMode = UIToggle.current.value; }
}
