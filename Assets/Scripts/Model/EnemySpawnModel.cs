using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnModel : IEnemySpawnModel
{
    public Guid Id { get; } = Guid.NewGuid();
    public Guid BuildingId { get; } = Guid.NewGuid();

    public List<Guid> SpawnQueue = new();
    public PathNodeModel PathNode { get; set; }
    IEnumerable<Guid> IEnemySpawnModel.SpawnQueue => SpawnQueue;

}
