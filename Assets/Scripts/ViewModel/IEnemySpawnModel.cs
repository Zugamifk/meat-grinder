using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemySpawnModel : IIdentifiable
{
    IEnumerable<Guid> SpawnQueue { get; }
    Guid BuildingId { get; }
}
