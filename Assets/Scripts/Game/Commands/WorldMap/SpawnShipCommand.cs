using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class SpawnShipCommand : ICommand
{
    bool _isFriend;
    Vector3 _position;
    Guid _shipId;

    public SpawnShipCommand(Guid spawnId, bool isFriend, Vector3 position)
    {
        _shipId = spawnId;
        _isFriend = isFriend;
        _position = position;
    }

    public void Execute(GameModel model)
    {
        var ship = new ShipModel();
        ship.Key = "Test_Ship";
        ship.Id = _shipId;
        ship.IsFriend = _isFriend;
        ship.Movement.CurrentPosition = _position;
        model.Ships.AddItem(ship);

        Game.AddUpdater(_shipId, new ShipUpdater(_shipId)); ;
    }
}
