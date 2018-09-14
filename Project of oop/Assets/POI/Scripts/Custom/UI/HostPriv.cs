using UnityEngine;
using System;

public class HostPriv : MonoBehaviour
{

    void OnEnable()
    {
        OnAccessLevelChanged();
    }

    void OnAccessLevelChanged()
    {
            NGUITools.SetActiveChildren(gameObject, Convert.ToBoolean(PlayerPrefs.GetInt("Host", 0)));
    }
}
