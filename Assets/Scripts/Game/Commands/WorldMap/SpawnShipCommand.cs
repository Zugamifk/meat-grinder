using Codice.CM.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class SpawnShipCommand : ICommand
{
    protected bool _isFriend;
    protected Vector3 _position;
    protected Guid? _setId;

    public SpawnShipCommand(bool isFriend, Vector3 position, Guid? setId)
    {
        _setId = setId;
        _isFriend = isFriend;
        _position = position;
    }

    public void Execute(GameModel model)
    {
        var shipId = _setId.HasValue ? _setId.Value : Guid.NewGuid();
        var ship = new ShipModel();
        ship.Key = "Test_Ship";
        ship.Id = shipId;
        ship.IsFriend = _isFriend;
        ship.Movement.CurrentPosition = _position;
        model.Ships.AddItem(ship);

        Game.AddUpdater(shipId, new ShipUpdater(shipId)); ;
    }
}
