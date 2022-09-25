using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EndPortalModel : ITileStructure
{
    public string Key => "EndPortal";

    public Guid Id { get; } = Guid.NewGuid();
}
