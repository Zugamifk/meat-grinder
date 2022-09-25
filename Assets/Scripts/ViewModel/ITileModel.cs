using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITileModel
{
    ITileStructure Structure { get; }
    int Height { get; }
    bool HasPath { get; }
    IEdgeModel NorthEdge { get; }
    IEdgeModel SouthEdge { get; }
    IEdgeModel EastEdge { get; }
    IEdgeModel WestEdge { get; }
}
