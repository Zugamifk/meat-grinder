using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory;

namespace Demo
{
    public struct PopulateInventory : ICommand
    {
        public void Execute(GameModel model)
        {
            var item = new ItemModel()
            {
                DisplayName = "Building 1",
                ItemData = new BuildingItemModel()
            };
            model.Inventory.Items.AddItem(item);

            item = new ItemModel()
            {
                DisplayName = "Building 2",
                ItemData = new BuildingItemModel()
            };
            model.Inventory.Items.AddItem(item);

            item = new ItemModel()
            {
                DisplayName = "Building 3",
                ItemData = new BuildingItemModel()
            };
            model.Inventory.Items.AddItem(item);
        }
    }
}
