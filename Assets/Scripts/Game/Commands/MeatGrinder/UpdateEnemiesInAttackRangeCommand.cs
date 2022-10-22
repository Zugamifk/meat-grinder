using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateEnemiesInRangeCommand : ICommand
{
    Guid _weaponId;
    HashSet<Guid> _ids;

    public UpdateEnemiesInRangeCommand(Guid weaponId, HashSet<Guid> ids)
    {
        _weaponId = weaponId;
        _ids = ids;
    }

    public void Execute(GameModel model)
    {
        var building = model.Weapons.GetItem(_weaponId);
        building.TargetsInRange = _ids;
    }
}
