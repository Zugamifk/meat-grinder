using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTabItem : MonoBehaviour
{
    public Guid ItemId;
    public delegate void BuildTabItemDelegate(Guid itemId);
    public event BuildTabItemDelegate OnClickedItem;
    public void SetItem()
    {

    }
    public void OnClick()
    {
        OnClickedItem?.Invoke(ItemId);
    }
}
