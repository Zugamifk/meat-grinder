using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTurretModel : IBuilding
{
    public string Key => "GunTurret";

    public Guid Id { get; } = Guid.NewGuid();
}
