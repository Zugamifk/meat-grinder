using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEdgeModel
{
    public ETileType Type { get; }
    public ITileModel Tile { get; }
    public IEdgeModel Pair { get; }
}
