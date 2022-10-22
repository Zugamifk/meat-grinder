using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipModel : IShipModel
{
    public Guid Id { get; set; }

    public string Key { get; set; }

    public ShipMovementModel Movement { get; } = new();

    Vector3 IShipModel.Position => Movement.CurrentPosition;
    float IShipModel.Rotation => Movement.CurrentAngle;
}