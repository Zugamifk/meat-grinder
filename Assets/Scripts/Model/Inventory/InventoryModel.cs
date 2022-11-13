using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class InventoryModel : IInventoryModel
    {
        public IdentifiableCollection<ItemModel> Items { get; } = new();

        IEnumerable<IInventoryItemModel> IInventoryModel.GetItemsOfType<TInventoryItem>()
        {
            foreach (var item in Items.AllItems)
            {
                if (item.ItemData is TInventoryItem)
                {
                    yield return item;
                }
            }
        }

        IIdentifiableLookup<IInventoryItemModel> IInventoryModel.Items => Items;
    }
}