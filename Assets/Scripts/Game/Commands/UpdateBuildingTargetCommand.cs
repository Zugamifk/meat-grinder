using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateBuildingTargetCommand : ICommand
{
    Guid _id;
    public UpdateBuildingTargetCommand(Guid id) => _id = id;

    public void Execute(GameModel model)
    {
        if(_id == Guid.Empty)
        {
            throw new InvalidOperationException($"Nothing to spawn! Id is empty!");
        }

        var building = model.Buildings.GetItem(_id);
        if(building== null)
        {
            throw new InvalidOperationException($"No building with id {_id}");
        }

        var weapon = model.Weapons.GetItem(_id);
        if (weapon == null)
        {
            throw new InvalidOperationException($"No weapon with id {_id}");
        }

        var pos = building.WorldPosition;
        Guid closest = Guid.Empty;
        float closestDistance = float.MaxValue;
        foreach(var id in weapon.TargetsInRange)
        {
            if(closest == Guid.Empty)
            {
                closest = id;
                continue;
            }

            var enemy = model.SpawnedEnemies.GetItem(id);
            if (enemy == null) continue;

            var distance = (enemy.Position - pos).magnitude;
            if(closestDistance > distance)
            {
                closest = id;
                closestDistance = distance;
            }
        }

        weapon.CurrentTarget = closest;
    }
}
