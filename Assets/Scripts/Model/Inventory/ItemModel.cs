using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public sealed class ItemModel : IInventoryItemModel
    {
        public string DisplayName { get; set; }

        public string Key => DisplayName;

        public Guid Id { get; set; } = Guid.NewGuid();

        public IInventoryItemDataModel ItemData { get; set; }
    }
}