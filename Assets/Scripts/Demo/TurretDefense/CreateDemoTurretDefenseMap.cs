using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo
{
    public class CreateDemoTurretDefenseMap : ICommand
    {
        public void Execute(GameModel model)
        {
            model.ShipMap = new();

            var generator = new MapGenerator();
            generator.GenerateMap(model);
            generator.GeneratePath(model,
                new Vector2Int(10, 0),
                new Vector2Int(7, 0),
                new Vector2Int(7, 5),
                new Vector2Int(3, 5),
                new Vector2Int(3, -8),
                new Vector2Int(-3, -8),
                new Vector2Int(-3, 5),
                new Vector2Int(-7, 5),
                new Vector2Int(-7, 0),
                new Vector2Int(-10, 0));

            var start = model.ShipMap.Paths.StartNode.Position;
            Game.Do(new CreateEnemySpawnCommand(start));
            var end = model.ShipMap.Paths.EndNode.Position;
            Game.Do(new SpawnBuildingCommand("EndPortal", end));
        }
    }
}
