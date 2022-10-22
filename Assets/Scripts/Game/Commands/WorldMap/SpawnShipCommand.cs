using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class SpawnShipCommand : ICommand
{
    Guid _spawnId;
    public SpawnShipCommand(Guid spawnId)
    {
        _spawnId = spawnId;
    }

    public void Execute(GameModel model)
    {
        var ship = new ShipModel();
        ship.Key = "Test_Ship";
        ship.Id = _spawnId;
        model.Ships.AddItem(ship);
    }
}
