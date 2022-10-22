using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovementModel
{
    public Vector3? TargetPosition { get; set; }
    public Vector3 CurrentPosition { get; set; }
    public float CurrentMoveSpeed { get; set; } = 0;
    public float MaxMoveSpeed { get; set; } = 5;
    public float Acceleration { get; set; } = 5;
}
