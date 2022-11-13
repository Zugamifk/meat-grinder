using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BuildTabContent : TabContent
{
    [SerializeField]
    BuildTabItem _itemTemplate;
    [SerializeField]
    Transform _itemListRoot;

    IObjectPool<BuildTabItem> _itemPool;
    List<BuildTabItem> _visibleItems = new();

    private void Awake()
    {
         _itemPool = new ObjectPool<BuildTabItem>(InstantiateTemplate);
    }

    public override void OnShowTab()
    {
        Debug.Log("Showing!");
        PopulateItems();
    }

    void PopulateItems()
    {
        Clear();

        foreach(var item in Game.Model.Inventory.GetItemsOfType<IBuildableItem>())
        {
            AddItem(item);
        }
    }

    void AddItem(IInventoryItemModel item)
    {
        var uiItem = _itemPool.Get();
        var ident = uiItem.GetComponent<Identifiable>();
        ident.Id = item.Id;
        uiItem.UpdateItem();
        uiItem.gameObject.SetActive(true);
    }

    void Clear()
    {
        foreach(var item in _visibleItems)
        {
            item.gameObject.SetActive(false);
            _itemPool.Release(item);
        }
    }

    BuildTabItem InstantiateTemplate()
    {
        var item = Instantiate(_itemTemplate);
        item.gameObject.SetActive(false);
        item.transform.SetParent(_itemListRoot);
        return item;
    }

}
