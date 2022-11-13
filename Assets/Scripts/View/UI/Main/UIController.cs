using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    TMPro.TextMeshProUGUI _tabTitleText;

    Dictionary<string, TabController> _tabKeyToTab = new();

    public void ToggleControlModes_OnClick()
    {

    }

    public void ShowTab_OnClick(string key)
    {
        if(!_tabKeyToTab.TryGetValue(key, out TabController tab))
        {
            throw new System.ArgumentException($"No tab with key {key}!");
        }

        _tabTitleText.text = key;
        tab.ShowTab();
    }
}
