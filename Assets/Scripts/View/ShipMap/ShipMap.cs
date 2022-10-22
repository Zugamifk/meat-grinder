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
        if (_mapGuid != map.Id)
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

                tile.SetTile(map.GetTile(new Vector2Int(x, y)));
                _positionToTile[new Vector2Int(x, y)] = tile;
            }
        }

        _mapGuid = map.Id;
    }

    void OnSpawnedBuilding(IBuildingModel model, BuildingView view)
    {
        var tile = _positionToTile[model.TilePosition];
        tile.AddBuilding(view);
    }
}
