using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileModel : ITileModel
{
    public ITileStructure Structure { get; set; }
    public int Height { get; set; }
    public bool HasPath { get; set; }
    public EdgeModel NorthEdge { get; set; }
    public EdgeModel SouthEdge { get; set; }
    public EdgeModel EastEdge { get; set; }
    public EdgeModel WestEdge { get; set; }

    IEdgeModel ITileModel.NorthEdge => NorthEdge;
    IEdgeModel ITileModel.SouthEdge => SouthEdge;
    IEdgeModel ITileModel.EastEdge => EastEdge;
    IEdgeModel ITileModel.WestEdge => WestEdge;
}
