using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTurretModel : ITileStructure
{
    public string Key => "GunTurret";

    public Guid Id { get; } = Guid.NewGuid();
}
