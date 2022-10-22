using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectileModel : IIdentifiable, IKeyHolder
{
    Vector3 Position { get; }
    Guid TargetEnemyId { get; }
}
