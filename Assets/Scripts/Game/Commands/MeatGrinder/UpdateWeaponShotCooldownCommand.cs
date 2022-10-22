using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateWeaponShotCooldownCommand : ICommand
{
    Guid _id;

    public UpdateWeaponShotCooldownCommand(Guid id) => _id = id;

    public void Execute(GameModel model)
    {
        var weapon = model.Weapons.GetItem(_id);
        weapon.ShotTimer = MathF.Max(weapon.ShotTimer - model.TimeModel.LastDeltaTime, 0);
    }
}
