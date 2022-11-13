using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateShipInteriorCommand : ICommand
{
    ShipData _shipData;

    public GenerateShipInteriorCommand(ShipData shipData)
    {
        _shipData = shipData;
    }

    public void Execute(GameModel model)
    {
        model.ShipMap = new();

        var generator = new ShipInteriorGenerator(_shipData);
        generator.GenerateMap(model);

        var start = model.ShipMap.Paths.StartNode.Position;
        Game.Do(new CreateEnemySpawnCommand(start));
        var end = model.ShipMap.Paths.EndNode.Position;
        Game.Do(new SpawnBuildingCommand("EndPortal", end));
    }
}
