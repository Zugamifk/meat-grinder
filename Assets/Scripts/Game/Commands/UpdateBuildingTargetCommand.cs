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

        var pos = building.WorldPosition;
        Guid closest = Guid.Empty;
        float closestDistance = float.MaxValue;
        foreach(var id in building.TargetsInRange)
        {
            if(closest == Guid.Empty)
            {
                closest = id;
                continue;
            }

            var enemy = model.SpawnedEnemies.GetItem(id);
            var distance = (enemy.Position - pos).magnitude;
            if(closestDistance > distance)
            {
                closest = id;
                closestDistance = distance;
            }
        }

        building.CurrentTarget = closest;
    }
}
