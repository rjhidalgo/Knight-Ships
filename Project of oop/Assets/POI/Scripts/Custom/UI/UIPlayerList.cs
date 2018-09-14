using UnityEngine;

/// <summary>
/// This script creates a visible list of players that are currently present in the game.
/// the "list" is just a single player since the game is not networked.
/// </summary>

public class UIPlayerList : MonoBehaviour
{
	// Tween triggered on click
	public TweenPosition tween;

	// Player entry prefab
	public GameObject prefab;

	// Instantiated prefab
	UIPlayerName mPlayer = null;
	bool mShown = false;

	/// <summary>
	/// Add the player to the list.
	/// </summary>

	void Start ()
	{

		GameObject parent = tween.gameObject;

		// Add the player.
		GameObject go = NGUITools.AddChild(tween.gameObject, prefab);
		mPlayer = go.GetComponent<UIPlayerName>();
		mPlayer.playerName = PlayerProfile.playerName;
		mPlayer.UpdateInfo(mShown);

        // Add the player.
        GameObject bo = NGUITools.AddChild(tween.gameObject, prefab);
        mPlayer = bo.GetComponent<UIPlayerName>();
        mPlayer.playerName = PlayerProfile.playerName;
        mPlayer.UpdateInfo(mShown);

        // Make sure that the tweened object has a collider
        NGUITools.AddWidgetCollider(parent);
		UIEventListener.Get(parent).onClick = ToggleList;

		// Refresh everything immediately so that there is no visible delay
		parent.BroadcastMessage("CreatePanel", SendMessageOptions.DontRequireReceiver);
		UIPanel pnl = NGUITools.FindInParents<UIPanel>(gameObject);
		if (pnl != null) pnl.Refresh();
	}

    private void Update()
    {
        
    }

    /// <summary>
    /// Show / hide the list of players.
    /// </summary>

    void ToggleList (GameObject go)
	{
		mShown = !mShown;
		tween.Toggle();
		mPlayer.UpdateInfo(mShown);
	}
}
