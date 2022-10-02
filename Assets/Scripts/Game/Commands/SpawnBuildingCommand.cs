using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBuildingCommand : ICommand
{
    string _buildingKey;
    Vector2Int _buildingPosition;
    Guid? _customGuid;
    public SpawnBuildingCommand(string buildingKey, Vector2Int position, Guid? customGuid = null)
    {
        _buildingKey = buildingKey;
        _buildingPosition = position;
        _customGuid = customGuid;
    }

    public void Execute(GameModel model)
    {
        var building = new BuildingModel()
        {
            Key = _buildingKey,
            TilePosition = _buildingPosition
        };
        if (_customGuid.HasValue)
        {
            building.Id = _customGuid.Value;
        }
        model.Buildings.AddItem(building);
    }
}
