using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingModel : IBuildingModel
{
    public string Key { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();
    public Vector2Int TilePosition { get; set; }
}
