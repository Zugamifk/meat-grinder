using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class SpawnShipCommand : ICommand
{
    Guid _shipId;
    public SpawnShipCommand(Guid spawnId)
    {
        _shipId = spawnId;
    }

    public void Execute(GameModel model)
    {
        var ship = new ShipModel();
        ship.Key = "Test_Ship";
        ship.Id = _shipId;
        model.Ships.AddItem(ship);

        Game.AddUpdater(_shipId, new ShipUpdater(_shipId)); ;
    }
}
