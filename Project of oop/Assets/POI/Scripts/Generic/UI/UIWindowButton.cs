using UnityEngine;

public class UIWindowButton : MonoBehaviour
{
	public enum Action
	{
		Show,
		Hide,
		GoBack,
	}

	public UIPanel window;
    public UILabel LoginMessage;
	public Action action = Action.Hide;
	public bool requiresFullVersion = false;
	public bool eraseHistory = false;

	void Start ()
	{
		UIPanel panel = NGUITools.FindInParents<UIPanel>(gameObject);

		if (panel != null)
		{
			UIWindow.Add(panel);
		}
	}

	void OnClick ()
	{

		switch (action)
		{
			case Action.Show:
			{
				if (window != null)
				{
                        if (LoginMessage == null)
                        {
                            if (eraseHistory) UIWindow.Close();
                            UIWindow.Show(window);
                        }
                        else
                        {

                            if (eraseHistory) UIWindow.Close();

                            if (LoginMessage.text.Equals("Successfully Logged In"))
                                UIWindow.Show(window);
                        }

				}
			}
			break;

			case Action.Hide:
			UIWindow.Close();
			break;

			case Action.GoBack:
                {
                    if (LoginMessage == null)
                        UIWindow.GoBack();
                    else
                    {
                        if (LoginMessage.text.CompareTo("passwords don't match") != 0)
                            UIWindow.GoBack();
                    }
                }
			break;
		}
	}
}
