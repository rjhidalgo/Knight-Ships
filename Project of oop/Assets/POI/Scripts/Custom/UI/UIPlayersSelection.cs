using UnityEngine;

[RequireComponent(typeof(UIPopupList))]
public class UIPlayersSelection : MonoBehaviour
{
	public UIPopupList oceanSize;

	UIPopupList mAllowed;

	void Awake ()
	{
		mAllowed = GetComponent<UIPopupList>();
	}

	void Start ()
	{
		EventDelegate.Add(oceanSize.onChange, OnSelection);
		OnSelection();
	}

	void OnEnable () { OnSelection(); }

	void OnSelection ()
	{
		string oceanType = oceanSize.value;
		string mySelection = mAllowed.value;
		mAllowed.items.Clear();

		if (oceanType == "Small")
		{
			mAllowed.items.Add("2");
			mAllowed.items.Add("3");
            mAllowed.items.Add("4");
            mAllowed.items.Add("5");
            mAllowed.items.Add("6");
            mAllowed.items.Add("7");
            mAllowed.items.Add("8");
        }
		else if (oceanType == "Medium" || oceanType == "Defensible")
		{
			mAllowed.items.Add("2");
			mAllowed.items.Add("3");
			mAllowed.items.Add("4");
			mAllowed.items.Add("5");
			mAllowed.items.Add("2 vs 2");
		}
		else
		{
			mAllowed.items.Add("2");
			mAllowed.items.Add("3");
			mAllowed.items.Add("4");
			mAllowed.items.Add("5");
			mAllowed.items.Add("6");
			mAllowed.items.Add("2 vs 2");
			mAllowed.items.Add("3 vs 3");
			mAllowed.items.Add("2 vs 2 vs 2");
		}
		mAllowed.value = mAllowed.items.Contains(mySelection) ? mySelection : "2";
	}
}
