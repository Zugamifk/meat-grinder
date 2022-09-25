using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator;

public class MapMeshGenerator
{
    MeshBuilder _builder = new();
    public Mesh GenerateTileMesh(ITileModel tile)
    {
        var data = DataService.GetData<GameData>();
        var grassColor = data.GrassColor;
        _builder.SetColor(grassColor);

        var w = .5f;
        var h = tile.Height * data.TileStepHeight;

        // edges and bottom
        var p0 = new Vector3(-w, 0, -w);
        var p1 = new Vector3(-w, 0, w);
        var p2 = new Vector3(w, 0, w);
        var p3 = new Vector3(w, 0, -w);
        var p4 = new Vector3(-w, h, -w);
        var p5 = new Vector3(-w, h, w);
        var p6 = new Vector3(w, h, w);
        var p7 = new Vector3(w, h, -w);

        _builder.AddQuad(p0, p3, p2, p1);
        _builder.AddQuad(p0, p4, p7, p3);
        _builder.AddQuad(p0, p1, p5, p4);
        _builder.AddQuad(p1, p2, p6, p5);
        _builder.AddQuad(p3, p7, p6, p2);

        // top, with road slices
        var r = w * data.RoadWidth;
        var r0 = new Vector3(-r, h, -r);
        var r1 = new Vector3(-r, h, r);
        var r2 = new Vector3(r, h, r);
        var r3 = new Vector3(r, h, -r);

        var p45r0 = new Vector3(-w, h, -r);
        var p45r1 = new Vector3(-w, h, r);
        var p56r1 = new Vector3(-r, h, w);
        var p56r2 = new Vector3(r, h, w);
        var p67r2 = new Vector3(w, h, r);
        var p67r3 = new Vector3(w, h, -r);
        var p74r3 = new Vector3(r, h, -w);
        var p74r0 = new Vector3(-r, h, -w);

        _builder.AddQuad(p4, p45r0, r0, p74r0);
        _builder.AddQuad(p5, p56r1, r1, p45r1);
        _builder.AddQuad(p6, p67r2, r2, p56r2);
        _builder.AddQuad(p7, p74r3, r3, p67r3);

        var roadColor = data.PathColor;
        _builder.SetColor(tile.HasPath ? roadColor : grassColor);
        _builder.AddQuad(r0, r1, r2, r3);

        _builder.SetColor(GetEdgeColor(data, tile.WestEdge.Type));
        _builder.AddQuad(p45r0, p45r1, r1, r0);

        _builder.SetColor(GetEdgeColor(data, tile.NorthEdge.Type));
        _builder.AddQuad(p56r1, p56r2, r2, r1);

        _builder.SetColor(GetEdgeColor(data, tile.EastEdge.Type));
        _builder.AddQuad(p67r2, p67r3, r3, r2);

        _builder.SetColor(GetEdgeColor(data, tile.SouthEdge.Type));
        _builder.AddQuad(p74r3, p74r0, r0, r3);
        return _builder.BuildMesh();
    }

    Color GetEdgeColor(GameData data, EMapTileEdgeType edge)
    {
        return edge switch
        {
            EMapTileEdgeType.Path => data.PathColor,
            EMapTileEdgeType _ => data.GrassColor,
        };
    }
}