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
        model.Bounds = new BoundsInt(-w / 2, -h / 2, 0, w, h, 1);

        for (int x = -w / 2; x <= w / 2; x++)
        {
            for (int y = -h / 2; y <= h / 2; y++)
            {
                model.TileMap[new Vector2Int(x, y)] = CreateTile(x, y, model);
            }
        }
    }

    TileModel CreateTile(int x, int y, MapModel map)
    {
        var tile = new TileModel()
        {
            Height = 1
        };
        
        tile.NorthEdge = CreateEdge(tile);
        tile.SouthEdge = CreateEdge(tile);
        if(y > map.Bounds.yMin)
        {
            ConfigureNeighbourEdges(tile.SouthEdge, map.TileMap[new Vector2Int(x, y - 1)].NorthEdge);
        }
        tile.EastEdge = CreateEdge(tile);
        tile.WestEdge = CreateEdge(tile);
        if (x > map.Bounds.xMin)
        {
            ConfigureNeighbourEdges(tile.WestEdge, map.TileMap[new Vector2Int(x-1, y)].EastEdge);
        }
        return tile;
    }

    EdgeModel CreateEdge(TileModel tile)
    {
        return new EdgeModel()
        {
            Type = EMapTileEdgeType.Wall,
            Tile = tile,
        };
    }

    void ConfigureNeighbourEdges(EdgeModel left, EdgeModel right)
    {
        left.Pair = right;
        right.Pair = left;
        left.Type = EMapTileEdgeType.Empty;
        right.Type = EMapTileEdgeType.Empty;
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
                var tile = model.TileMap[new Vector2Int(x, y)];
                tile.HasPath = true;
                UpdateEdge(tile.NorthEdge);
                UpdateEdge(tile.SouthEdge);
                UpdateEdge(tile.EastEdge);
                UpdateEdge(tile.WestEdge);
            }
        }

        void UpdateEdge(EdgeModel edge)
        {
            if (edge.Pair == null || !edge.Pair.Tile.HasPath) return;
            edge.Type = EMapTileEdgeType.Path;
            edge.Pair.Type = EMapTileEdgeType.Path;
        }
    }
}
