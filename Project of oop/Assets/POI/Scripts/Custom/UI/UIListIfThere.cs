using UnityEngine;

public class UIListIfThere : MonoBehaviour
{
    public UILabel playerNum;

    void OnEnable()
    {
        OnAccessLevelChanged();
    }

    void OnAccessLevelChanged()
    {   
        NGUITools.SetActiveChildren(gameObject, playerNum.text != "Unknown");
    }
}
