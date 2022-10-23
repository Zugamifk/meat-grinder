using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpdater : IUpdater
{
    Guid _id;

    public WeaponUpdater(Guid id)
    {
        _id = id;
    }

    public void Update(GameModel model)
    {
        UpdateCurrentTarget(model);
        UpdateShotCooldown(model);
    }

    void UpdateCurrentTarget(GameModel model)
    {
        var building = model.Buildings.GetItem(_id);
        if (building == null)
        {
            throw new InvalidOperationException($"No building with id {_id}");
        }

        var weapon = model.Weapons.GetItem(_id);
        if (weapon == null)
        {
            throw new InvalidOperationException($"No weapon with id {_id}");
        }

        var vision = model.Vision.GetItem(_id);
        if (vision == null)
        {
            throw new InvalidOperationException($"No vision with id {_id}");
        }

        var pos = building.WorldPosition;
        Guid closest = Guid.Empty;
        float closestDistance = float.MaxValue;
        foreach (var id in vision.ObjectsInRange)
        {
            var enemy = model.SpawnedEnemies.GetItem(id);
            if (enemy == null) continue;

            if (closest == Guid.Empty)
            {
                closest = id;
                continue;
            }

            var distance = (enemy.Position - pos).magnitude;
            if (closestDistance > distance)
            {
                closest = id;
                closestDistance = distance;
            }
        }

        weapon.CurrentTarget = closest;
    }

    void UpdateShotCooldown(GameModel model)
    {
        var weapon = model.Weapons.GetItem(_id);
        weapon.ShotTimer = MathF.Max(weapon.ShotTimer - model.TimeModel.LastDeltaTime, 0);
    }
}
