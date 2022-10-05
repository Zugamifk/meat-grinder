using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyViewSpawner : RegisteredPrefabViewSpawner<IEnemyModel, Enemy>
{
    protected override IEnumerable<IEnemyModel> AllModels() => Game.Model.SpawnedEnemies.AllItems;

    protected override IEnemyModel GetModel(Guid id) => Game.Model.SpawnedEnemies.GetItem(id);

    protected override void SpawnedView(IEnemyModel model, Enemy view)
    {
        //var enemyPrefab = Prefabs.GetInstance(model);
        //enemyPrefab.transform.SetParent(Test.Instance.SpawnRoot);
        //enemyPrefab.transform.position = _spawnPosition.position;

        //var identifiable = enemyPrefab.GetComponent<Identifiable>();
        //identifiable.Id = enemy.Id;

        ViewLookup.Register(model.Id, view.gameObject);
    }

    protected override void DestroyedView(Enemy view)
    {
        ViewLookup.Remove(view.Id);
    }
}
