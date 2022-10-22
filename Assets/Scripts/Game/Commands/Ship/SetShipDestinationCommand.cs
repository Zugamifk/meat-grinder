using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetShipDestinationCommand : ICommand
{
    Guid _shipId;
    Vector3 _position;

    public SetShipDestinationCommand(Guid shipId, Vector3 position)
    {
        _shipId = shipId;
        _position = position;
    }

    public void Execute(GameModel model)
    {
        var ship = model.Ships.GetItem(_shipId);
        ship.Movement.TargetPosition = _position;
    }
}
