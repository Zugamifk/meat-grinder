using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator
{
    public void GenerateMap(GameModel model)
    {
        FillEmptyMap(model.Map);
        GeneratePath(model);
    }

    void FillEmptyMap(MapModel model)
    {
        var data = DataService.GetData<GameData>();
        var w = data.Dimensions.x;
        var h = data.Dimensions.y;
        for (int x = -w / 2; x <= w / 2; x++)
        {
            for (int y = -h / 2; y <= h / 2; y++)
            {
                model.TileMap[new Vector2Int(x, y)] = new TileModel()
                {
                    HasPath = false,
                    Height = Random.Range(1, 3)
                };
            }
        }

        model.Bounds = new BoundsInt(-w / 2, -h / 2, 0, w, h, 1);
    }

    void GeneratePath(GameModel model)
    {
        var end = new PathNodeModel() { Position = new Vector2Int() };
        var start = new PathNodeModel() { Position = new Vector2Int(0, 10), Next = end };
        var map = model.Map;
        map.Paths.StartNode = start;
        map.Paths.EndNode = end;

        GenerateTilePaths(map);

        map.TileMap[end.Position].Structure = new EndPortalModel();
        var spawn = new EnemySpawnModel();
        spawn.PathNode = start;
        model.Spawns.AddItem(spawn);
        map.TileMap[start.Position].Structure = spawn;
    }

    void GenerateTilePaths(MapModel model)
    {
        for (var start = model.Paths.StartNode; start != model.Paths.EndNode; start = start.Next)
        {
            GenerateNodePath(model, start.Position, start.Next.Position);
        }
    }

    void GenerateNodePath(MapModel model, Vector2Int start, Vector2Int end)
    {
        int xd = (int)Mathf.Sign(end.x - start.x);
        int yd = (int)Mathf.Sign(end.y - start.y);
        for (int x = start.x; x != end.x + xd; x += xd)
        {
            for (int y = start.y; y != end.y + yd; y += yd)
            {
                model.TileMap[new Vector2Int(x, y)].HasPath = true;
            }
        }
    }
}
