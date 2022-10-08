using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileModel : IProjectileModel
{
    public float Velocity {get;set;}

    public Vector3 Position { get;set;}

    public Guid Id { get; }= Guid.NewGuid();

    public string Key { get; set; }
    public Guid TargetEnemyId { get; set; }
}
