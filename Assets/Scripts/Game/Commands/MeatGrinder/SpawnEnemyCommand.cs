using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyCommand : ICommand
{
    Guid _spawnId;

    public SpawnEnemyCommand(Guid spawnId)
    {
        _spawnId = spawnId;
    }

    public void Execute(GameModel model)
    {
        var spawn = model.Spawns.GetItem(_spawnId);
        var enemy = new EnemyModel();
        enemy.Movement.CurrentNode = spawn.PathNode.Next;
        enemy.Movement.CurrentPosition = spawn.PathNode.WorldPosition;
        model.SpawnedEnemies.AddItem(enemy);
        spawn.SpawnQueue.Add(enemy.Id);

        Game.AddUpdater(enemy.Id, new EnemyUpdater(enemy.Id));
    }
}
