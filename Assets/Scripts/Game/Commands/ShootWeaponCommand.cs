using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootWeaponCommand : ICommand
{
    Guid _id;
    Vector3 _spawnPosition;
    public ShootWeaponCommand(Guid id, Vector3 spawnPosition) { 
        _id = id; 
        _spawnPosition = spawnPosition; 
    }

    public void Execute(GameModel model)
    {
        var weapon = model.Weapons.GetItem(_id);
        var weaponData = DataService.GetData<WeaponDataCollection>().GetWeapon(weapon.Key);

        // create projectile
        var enemy = model.SpawnedEnemies.GetItem(weapon.CurrentTarget);
        var projectile = new ProjectileModel()
        {
            Key = "Projectile",
            Position = _spawnPosition,
            Velocity = weaponData.ProjectileSpeed,
            TargetEnemyId = enemy.Id
        };
        model.Projectiles.AddItem(projectile);

        weapon.ShotTimer += weaponData.ShotCooldown;
    }
}
