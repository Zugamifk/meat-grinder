using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponModel : IWeaponModel
{
    public Guid Id { get; set; }
    public string Key {get;set;}
    public HashSet<Guid> TargetsInRange { get; set; } = new();
    public Guid CurrentTarget { get; set; }
    public float ShotTimer { get; set; }
}
