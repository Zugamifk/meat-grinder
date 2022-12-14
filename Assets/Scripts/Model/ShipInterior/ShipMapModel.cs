using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMapModel : IShipMapModel
{
    public Dictionary<Vector2Int, TileModel> TileMap = new();

    public Guid Id { get; } = Guid.NewGuid();

    public BoundsInt Bounds { get; set; }
    public PathModel Paths = new();

    public ITileModel GetTile(Vector2Int position) => TileMap[position];
}
