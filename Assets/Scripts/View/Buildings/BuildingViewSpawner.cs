using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingViewSpawner : RegisteredPrefabViewSpawner<IBuildingModel, BuildingView>
{
    public event Action<IBuildingModel, BuildingView> OnSpawnedBuilding;

    protected override IEnumerable<IBuildingModel> AllModels() => Game.Model.Buildings.AllItems;

    protected override IBuildingModel GetModel(Guid id) => Game.Model.Buildings.GetItem(id);

    protected override void SpawnedView(IBuildingModel model, BuildingView view)
    {
        ViewLookup.Register(model.Id, view.gameObject);
        OnSpawnedBuilding?.Invoke(model, view);
    }

    protected override void DestroyedView(BuildingView view)
    {
        ViewLookup.Remove(view.Id);
    }
}
