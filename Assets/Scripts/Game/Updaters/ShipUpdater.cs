using Codice.Client.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipUpdater : IUpdater
{
    Guid _id;
    public ShipUpdater(Guid id)
    {
        _id = id;
    }

    public void Update(GameModel model)
    {
        var ship = model.Ships.GetItem(_id);
        MoveToPosition(model, ship);
    }

    void MoveToPosition(GameModel model, ShipModel ship)
    {
        var movement = ship.Movement;
        if(!movement.TargetPosition.HasValue)
        {
            return;
        }

        if(movement.CurrentMoveSpeed < movement.MaxMoveSpeed)
        {
            movement.CurrentMoveSpeed = Mathf.Min(movement.CurrentMoveSpeed + movement.Acceleration * model.TimeModel.LastDeltaTime, movement.MaxMoveSpeed);
        }

        var step = movement.CurrentMoveSpeed * model.TimeModel.LastDeltaTime;
        var to = movement.TargetPosition.Value - movement.CurrentPosition;
        if(to.magnitude < step)
        {
            movement.CurrentPosition = movement.TargetPosition.Value;
            movement.TargetPosition = null;
        } else
        {
            movement.CurrentPosition += to.normalized * step;
        }
    }
}
