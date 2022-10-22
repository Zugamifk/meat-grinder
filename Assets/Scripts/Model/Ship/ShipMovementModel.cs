using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovementModel
{
    // set values
    public Vector3? TargetPosition { get; set; }
    public float? TargetRotation { get; set; }

    // move state
    public Vector3 CurrentPosition { get; set; }
    public float CurrentMoveSpeed { get; set; } = 0;
    public float CurrentAngle { get; set; } = 0;

    // ship stats from ship data
    public float MaxMoveSpeed { get; set; } = 5;
    public float Acceleration { get; set; } = 5;
    public float TurnSpeed { get; set; } = 45;
}
