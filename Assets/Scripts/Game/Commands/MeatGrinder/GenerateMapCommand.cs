using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMapCommand : ICommand
{
    public void Execute(GameModel model)
    {
        model.ShipMap = new();

        var generator = new MapGenerator();
        generator.GenerateMap(model);

        var start = model.ShipMap.Paths.StartNode.Position;
        Game.Do(new CreateEnemySpawnCommand(start));
        var end = model.ShipMap.Paths.EndNode.Position;
        Game.Do(new SpawnBuildingCommand("EndPortal", end));
    }
}
