using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipViewSpawner : RegisteredPrefabViewSpawner<IShipModel, Ship>
{
    protected override IEnumerable<IShipModel> AllModels() => Game.Model.Ships.AllItems;

    protected override IShipModel GetModel(Guid id) => Game.Model.Ships.GetItem(id);

    protected override void SpawnedView(IShipModel model, Ship view)
    {
        ViewLookup.Register(model.Id, view.gameObject);
    }

    protected override void DestroyedView(Ship view)
    {
        ViewLookup.Remove(view.Id);
    }
}
