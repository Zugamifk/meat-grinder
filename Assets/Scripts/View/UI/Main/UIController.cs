using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    TabContainer _tabContainer;
    [SerializeField]
    List<TabContent> _tabs = new();

    public void ToggleControlModes_OnClick()
    {
        Game.Do(new ToggleNavigationOrInteriorControlMode());
    }

    public void ShowTab_OnClick(int index)
    {
        var tab = _tabs[index];
        _tabContainer.ShowTab(tab);
    }
}
