using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeModel
{
    public EMapTileEdgeType Type { get; set; }  
    public TileModel Tile { get; set; }
    public EdgeModel Pair { get; set; } 
}
