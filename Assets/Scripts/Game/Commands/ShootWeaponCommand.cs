using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootWeaponCommand : ICommand
{
    Guid _id;
    public ShootWeaponCommand(Guid id) => _id = id;

    public void Execute(GameModel model)
    {
        var weapon = model.Weapons.GetItem(_id);
        var weaponData = DataService.GetData<WeaponDataCollection>().GetWeapon(weapon.Key);

        var building = model.Buildings.GetItem(_id);

        // create projectile
        var enemy = model.SpawnedEnemies.GetItem(weapon.CurrentTarget);
        var projectile = new ProjectileModel()
        {
            Key = "Projectile",
            Position = building.WorldPosition,
            Velocity = weaponData.ProjectileSpeed,
            TargetEnemyId = enemy.Id
        };
        model.Projectiles.AddItem(projectile);

        weapon.ShotTimer += weaponData.ShotCooldown;
    }
}
