using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipInteriorGenerator
{
    ShipData _data;
    public ShipInteriorGenerator(ShipData data)
    {
        _data = data;
    }

    public void GenerateMap(GameModel model)
    {
        FillEmptyMap(model.ShipMap);
    }

    void FillEmptyMap(ShipMapModel model)
    {
        var w = _data.Dimensions.x;
        var h = _data.Dimensions.y;
        model.Bounds = new BoundsInt(-w / 2, -h / 2, 0, w, h, 1);

        for (int x = -w / 2; x <= w / 2; x++)
        {
            for (int y = -h / 2; y <= h / 2; y++)
            {
                model.TileMap[new Vector2Int(x, y)] = CreateTile(x, y, model);
            }
        }
    }

    TileModel CreateTile(int x, int y, ShipMapModel map)
    {
        var tile = new TileModel()
        {
            Height = 1,
            Type = ETileType.Empty
        };

        tile.NorthEdge = CreateEdge(tile);
        tile.SouthEdge = CreateEdge(tile);
        if (y > map.Bounds.yMin)
        {
            var south = map.TileMap[new Vector2Int(x, y - 1)];
            ConnectNewTileEdges(tile.SouthEdge, south.NorthEdge);
        }
        tile.EastEdge = CreateEdge(tile);
        tile.WestEdge = CreateEdge(tile);
        if (x > map.Bounds.xMin)
        {
            var west = map.TileMap[new Vector2Int(x - 1, y)];
            ConnectNewTileEdges(tile.WestEdge, west.EastEdge);
        }
        return tile;
    }

    EdgeModel CreateEdge(TileModel tile)
    {
        var edge = new EdgeModel()
        {
            Type = ETileType.Wall,
            Tile = tile,
        };
        UpdateEdge(edge);
        return edge;
    }

    void ConnectNewTileEdges(EdgeModel leftEdge, EdgeModel rightEdge)
    {
        leftEdge.Pair = rightEdge;
        rightEdge.Pair = leftEdge;
        UpdateEdge(leftEdge);
        UpdateEdge(rightEdge);
    }

    void UpdateEdgeAndPair(EdgeModel edge)
    {
        UpdateEdge(edge);
        if (edge.Pair != null)
        {
            UpdateEdge(edge.Pair);
        }
    }

    void UpdateEdge(EdgeModel edge)
    {
        var tile = edge.Tile;
        var other = edge.Pair;
        if (other == null)
        {
            if(tile.Type == ETileType.Wall)
            {
                edge.Type = ETileType.Empty;
            } else
            {
                edge.Type = ETileType.Wall;
            }
            return;
        }
        var nextTile = other.Tile;
        if (nextTile.Type == ETileType.Wall && tile.Type != ETileType.Wall)
        {
            edge.Type = ETileType.Wall;
        }
        else if (tile.Type == ETileType.Path && nextTile.Type == ETileType.Path)
        {
            edge.Type = ETileType.Path;
        }
        else
        {
            edge.Type = ETileType.Empty;
        }
    }

    public void GeneratePath(GameModel model, params Vector2Int[] waypoints)
    {
        var map = model.ShipMap;
        PathNodeModel last = null;
        for (int i = waypoints.Length - 1; i >= 0; i--)
        {
            var node = new PathNodeModel() { Position = waypoints[i], Next = last };
            if (last == null)
            {
                map.Paths.EndNode = node;
            }
            else if (i == 0)
            {
                map.Paths.StartNode = node;
            }
            last = node;
        }

        GenerateTilePaths(map);
    }

    void GenerateTilePaths(ShipMapModel model)
    {
        for (var start = model.Paths.StartNode; start != model.Paths.EndNode; start = start.Next)
        {
            GenerateNodePath(model, start.Position, start.Next.Position);
        }
    }

    void GenerateNodePath(ShipMapModel model, Vector2Int start, Vector2Int end)
    {
        int xd = (int)Mathf.Sign(end.x - start.x);
        int yd = (int)Mathf.Sign(end.y - start.y);
        for (int x = start.x; x != end.x + xd; x += xd)
        {
            for (int y = start.y; y != end.y + yd; y += yd)
            {
                var tile = model.TileMap[new Vector2Int(x, y)];
                tile.Type = ETileType.Path;
                UpdateEdgeAndPair(tile.NorthEdge);
                UpdateEdgeAndPair(tile.SouthEdge);
                UpdateEdgeAndPair(tile.EastEdge);
                UpdateEdgeAndPair(tile.WestEdge);
            }
        }
    }
}
