using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMap : MonoBehaviour
{
    [SerializeField]
    MapTile _tilePrefab;

    Guid _mapGuid;

    Dictionary<Vector2Int, MapTile> _positionToTile = new();

    public MapTile GetTile(Vector2Int position) => _positionToTile[position];

    private void Update()
    {
        var map = Game.Model.Map;
        if (_mapGuid != map.Id)
        {
            UpdateMap(map);
        } 
        Game.Do(new UpdateMapCommand());
    }

    void UpdateMap(IMapModel map)
    {
        for (int x = map.Bounds.xMin; x <= map.Bounds.xMax; x++)
        {
            for (int y = map.Bounds.yMin; y <= map.Bounds.yMax; y++)
            {
                var tile = Instantiate(_tilePrefab);
                tile.name = $"Tile ({x}, {y})";
                tile.transform.parent = transform;
                tile.transform.position = new Vector3(x, 0, y);

                tile.SetTile(map.GetTile(new Vector2Int(x, y)));
                _positionToTile[new Vector2Int(x, y)] = tile;
            }
        }

        _mapGuid = map.Id;
    }
}
