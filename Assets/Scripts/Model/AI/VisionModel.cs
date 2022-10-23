using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionModel : IVisionModel
{
    public Guid Id { get; set; }
    public HashSet<Guid> ObjectsInRange { get; set; } = new HashSet<Guid>();
    public float Range { get; set; }

}
