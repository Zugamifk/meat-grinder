using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyModel : IEnemyModel
{
    public Guid Id { get; } = Guid.NewGuid();

    public string Key { get; set; } = "Enemy";
    public PathMovementModel Movement { get; } = new();

    public Vector3 Position => Movement.CurrentPosition;
    public Vector3 TargetOffset { get; set; }
}
