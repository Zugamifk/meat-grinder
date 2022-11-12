using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMap : MonoBehaviour
{
    [SerializeField]
    BuildingViewSpawner _buildingSpawner;
    [SerializeField]
    ShipMapTile _tilePrefab;

    Guid _mapGuid;

    Dictionary<Vector2Int, ShipMapTile> _positionToTile = new();
    delegate void OnSpawnedTileDelegate(ShipMapTile tile);
    Dictionary<Vector2Int, OnSpawnedTileDelegate> _positionToOnSpawnedTile = new();
    public ShipMapTile GetTile(Vector2Int position) => _positionToTile[position];

    private void Awake()
    {
        _buildingSpawner.OnSpawnedBuilding += OnSpawnedBuilding;
    }

    private IEnumerator Start()
    {
        while(Game.Model.ShipMap == null)
        {
            yield return null;
        }
        UpdateMap(Game.Model.ShipMap);
        _buildingSpawner.enabled = true;
    }

    private void Update()
    {
        var map = Game.Model.ShipMap;
        if (map!=null && _mapGuid != map.Id)
        {
            UpdateMap(map);
        } 
    }

    void UpdateMap(IShipMapModel map)
    {
        for (int x = map.Bounds.xMin; x <= map.Bounds.xMax; x++)
        {
            for (int y = map.Bounds.yMin; y <= map.Bounds.yMax; y++)
            {
                var tile = Instantiate(_tilePrefab);
                tile.name = $"Tile ({x}, {y})";
                tile.transform.parent = transform;
                tile.transform.position = new Vector3(x, 0, y);

                var position = new Vector2Int(x, y);
                tile.SetTile(map.GetTile(position));
                _positionToTile[position] = tile;

                if(_positionToOnSpawnedTile.TryGetValue(position, out OnSpawnedTileDelegate onSpawnedTile))
                {
                    onSpawnedTile.Invoke(tile);
                    _positionToOnSpawnedTile.Remove(position);
                }
            }
        }

        _mapGuid = map.Id;
    }

    void OnSpawnedBuilding(IBuildingModel model, BuildingView view)
    {
        if(_positionToTile.TryGetValue(model.TilePosition, out ShipMapTile tile))
        {
            tile.AddBuilding(view);
        } else
        {
            _positionToOnSpawnedTile[model.TilePosition] = t => t.AddBuilding(view);
        }
    }

}
