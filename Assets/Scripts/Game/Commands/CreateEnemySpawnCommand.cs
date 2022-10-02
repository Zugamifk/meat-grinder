using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemySpawnCommand : ICommand
{
    Vector2Int _position;
    public CreateEnemySpawnCommand(Vector2Int position)
    {
        _position = position;
    }

    public void Execute(GameModel model)
    {
        var spawn = new EnemySpawnModel();
        spawn.PathNode = model.Map.Paths.StartNode;
        model.Spawns.AddItem(spawn);

        Game.Do(new SpawnBuildingCommand(Buildings.ENEMY_SPAWN, _position, spawn.BuildingId));
    }
}
