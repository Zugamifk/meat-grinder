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
        OnSpawnedBuilding?.Invoke(model, view);
    }
}
