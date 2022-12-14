using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyViewSpawner : RegisteredPrefabViewSpawner<IEnemyModel, Enemy>
{
    protected override IEnumerable<IEnemyModel> AllModels() => Game.Model.Enemies.AllItems;

    protected override IEnemyModel GetModel(Guid id) => Game.Model.Enemies.GetItem(id);

    protected override void SpawnedView(IEnemyModel model, Enemy view)
    {
        ViewLookup.Register(model.Id, view.gameObject);
    }

    protected override void DestroyedView(Enemy view)
    {
        ViewLookup.Remove(view.Id);

        view.OnKilled();
    }
}
