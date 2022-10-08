using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateProjectileCommand : ICommand
{
    Guid _id;
    public UpdateProjectileCommand(Guid id) => _id = id;

    public void Execute(GameModel model)
    {
        var projectile = model.Projectiles.GetItem(_id);
        var enemy = model.SpawnedEnemies.GetItem(projectile.TargetEnemyId);
        var toEnemy = enemy.Position - projectile.Position;
        var stepDistance = projectile.Velocity * model.TimeModel.LastDeltaTime;
        if(toEnemy.magnitude < stepDistance)
        {
            Game.Do(new KillEnemyCommand(enemy.Id));
            model.Projectiles.RemoveItem(_id);
        } else
        {
            projectile.Position += toEnemy.normalized * stepDistance;
        }
    }
}
