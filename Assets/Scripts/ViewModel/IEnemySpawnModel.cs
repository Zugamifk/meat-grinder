using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemySpawnModel : ITileStructure, IIdentifiable
{
    IEnumerable<Guid> SpawnQueue { get; }
}
