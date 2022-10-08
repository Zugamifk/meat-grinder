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

        // create projectile

        weapon.ShotTimer += weaponData.ShotCooldown;
    }
}
