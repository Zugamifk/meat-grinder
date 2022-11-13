using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Identifiable))]
public class BuildTabItem : MonoBehaviour
{
    [SerializeField]
    TMPro.TextMeshProUGUI _titleText;

    public delegate void BuildTabItemDelegate(Guid itemId);
    public event BuildTabItemDelegate OnClickedItem;

    Identifiable _identifiable_cached;
    Identifiable _identifiable => _identifiable_cached ?? GetComponent<Identifiable>();

    public void UpdateItem()
    {
        var item = Game.Model.Inventory.Items.GetItem(_identifiable.Id);
        _titleText.text = item.DisplayName;
    }

    public void OnClick()
    {
        OnClickedItem?.Invoke(_identifiable.Id);
    }
}
