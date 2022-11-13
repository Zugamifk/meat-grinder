using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTabContent : TabContent
{
    [SerializeField]
    BuildTabItem _itemTemplate;
    public override void OnShowTab()
    {
        Debug.Log("Showing!");
        PopulateItems();
    }

    void PopulateItems()
    {

    }
}
