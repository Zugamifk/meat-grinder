using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNodeModel
{
    public Vector2Int Position;
    public PathNodeModel Next;
    public Vector3 WorldPosition => new Vector3(Position.x, 0, Position.y);
}
