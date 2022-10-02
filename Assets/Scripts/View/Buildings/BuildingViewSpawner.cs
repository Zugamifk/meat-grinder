using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingViewSpawner : RegisteredPrefabViewSpawner<IBuildingModel, BuildingView>
{
    protected override IEnumerable<IBuildingModel> AllModels() => Game.Model.Buildings.AllItems;

    protected override IBuildingModel GetModel(Guid id) => Game.Model.Buildings.GetItem(id);
}
