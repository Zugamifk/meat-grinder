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
        var buildingData = DataService.GetData<BuildingDataCollection>().GetBuilding(_buildingKey);
        var building = new BuildingModel()
        {
            Key = _buildingKey,
            TilePosition = _buildingPosition,
            WorldPosition = new Vector3(_buildingPosition.x, 0, _buildingPosition.y)
        };
        if (_customGuid.HasValue)
        {
            building.Id = _customGuid.Value;
        }
        model.Buildings.AddItem(building);

        if(buildingData.IsWeapon)
        {
            var weaponData = DataService.GetData<WeaponDataCollection>().GetWeapon(_buildingKey);
            var weapon = new WeaponModel()
            {
                Id = building.Id,
                Key = _buildingKey,
                ShotTimer = weaponData.ShotCooldown
            };
            model.Weapons.AddItem(weapon);

            new CreateVisionCommand(building.Id, 7).Execute(model);

            Game.AddUpdater(building.Id, new WeaponUpdater(building.Id));
        }
    }
}
