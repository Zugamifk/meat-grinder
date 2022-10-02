using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBuildingCommand : ICommand
{
    string _buildingKey;
    Vector2Int _buildingPosition;
    public SpawnBuildingCommand(string buildingKey, Vector2Int position)
    {
        _buildingKey = buildingKey;
        _buildingPosition = position;
    }

    public void Execute(GameModel model)
    {
        var building = new BuildingModel()
        {
            Key = _buildingKey,
            TilePosition = _buildingPosition
        };
        model.Buildings.AddItem(building);
    }
}
