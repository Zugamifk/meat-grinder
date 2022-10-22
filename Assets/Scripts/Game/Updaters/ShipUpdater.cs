using Codice.Client.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

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
        RotateToTarget(model, ship);
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

        ship.Movement.TargetRotation = Mathf.Rad2Deg * Mathf.Atan2(to.x, to.z);
    }

    void RotateToTarget(GameModel model, ShipModel ship)
    {
        if(!ship.Movement.TargetRotation.HasValue)
        {
            return;
        }

        var step = ship.Movement.TurnSpeed * model.TimeModel.LastDeltaTime;
        var diff = ship.Movement.TargetRotation.Value - ship.Movement.CurrentAngle;
        if(step > Mathf.Abs(diff))
        {
            ship.Movement.CurrentAngle = ship.Movement.TargetRotation.Value;
            ship.Movement.TargetRotation = null;
            return;
        }

        ship.Movement.CurrentAngle += Mathf.Sign(diff) * step;
    }
}
