using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EndPortalModel : IBuildingModel
{
    public string Key => "EndPortal";

    public Guid Id { get; } = Guid.NewGuid();
}
