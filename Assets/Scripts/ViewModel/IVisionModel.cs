using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVisionModel : IIdentifiable
{
    float Range { get; }
    IReadOnlyCollection<Guid> ObjectsInRange { get; }
}
