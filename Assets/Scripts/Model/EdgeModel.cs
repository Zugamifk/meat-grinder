using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeModel : IEdgeModel
{
    public EMapTileEdgeType Type { get; set; }  
    public TileModel Tile { get; set; }
    public EdgeModel Pair { get; set; }

    ITileModel IEdgeModel.Tile => Tile;
    IEdgeModel IEdgeModel.Pair => Pair;
}
