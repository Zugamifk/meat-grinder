using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMapCommand : ICommand
{
    public void Execute(GameModel model)
    {
        var generator = new MapGenerator();
        generator.GenerateMap(model);

        var start = model.Map.Paths.StartNode.Position;
        Game.Do(new CreateEnemySpawnCommand(start));
        var end = model.Map.Paths.EndNode.Position;
        Game.Do(new SpawnBuildingCommand("EndPortal", end));
    }
}
