using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryModel
{
    IIdentifiableLookup<IInventoryItemModel> Items { get; }
    IEnumerable<IInventoryItemModel> GetItemsOfType<TInventoryItem>();
}
