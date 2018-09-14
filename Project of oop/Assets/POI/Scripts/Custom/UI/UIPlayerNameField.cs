using UnityEngine;

/// <summary>
/// Attach this script to an input if you want it to represent the player's name.
/// </summary>

[RequireComponent(typeof(UIInput))]
public class UIPlayerNameField : MonoBehaviour
{
	UILabel mLabel;

	void Awake ()
	{
		mLabel = GetComponent<UILabel>();
        mLabel.text = PlayerProfile.playerName;
    }

	/*void OnEnable ()
	{
		mLabel.text = PlayerProfile.playerName;
	}

	void OnSubmit ()
	{
		string text = UIInput.current.value;
		PlayerProfile.playerName = text;
		if (text != PlayerProfile.playerName)
			mLabel.text = PlayerProfile.playerName;
	}*/
}
