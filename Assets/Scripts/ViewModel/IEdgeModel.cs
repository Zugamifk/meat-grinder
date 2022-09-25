using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEdgeModel
{
    public EMapTileEdgeType Type { get; }
    public ITileModel Tile { get; }
    public IEdgeModel Pair { get; }
}
