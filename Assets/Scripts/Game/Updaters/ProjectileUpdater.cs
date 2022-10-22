using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ProjectileUpdater : IUpdater
{
    Guid _id;

    public ProjectileUpdater(Guid id)
    {
        _id = id;
    }

    public void Update(GameModel model)
    {
        var projectile = model.Projectiles.GetItem(_id);
        var enemy = model.SpawnedEnemies.GetItem(projectile.TargetEnemyId);
        var enemyPosition = enemy.Position;
        var toEnemy = enemyPosition - projectile.Position;
        var stepDistance = projectile.Velocity * model.TimeModel.LastDeltaTime;
        if (toEnemy.magnitude < stepDistance)
        {
            Game.Do(new KillEnemyCommand(enemy.Id));
            model.Projectiles.RemoveItem(_id);
            Game.RemoveUpdater(_id);
        }
        else
        {
            projectile.Position += toEnemy.normalized * stepDistance;
        }
    }
}
