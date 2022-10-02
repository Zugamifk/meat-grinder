using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTurretModel : IBuildingModel
{
    public string Key => "GunTurret";

    public Guid Id { get; } = Guid.NewGuid();
}
