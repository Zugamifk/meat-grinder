using MeshGenerator;
using MeshGenerator.Wireframe;
using System;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

[MeshGenerator("Tile")]
public class TileMeshGenerator : MeshGeneratorWithData<TileMeshGeneratorData>
{
    ITileModel _tile;

    public void SetTile(ITileModel tile)
    {
        _tile = tile;
    }

    protected override MeshGeneratorResult BuildMesh()
    {
        var grassColor = Data.GrassColor;
        _builder.SetColor(grassColor);

        var w = .5f;
        var h = _tile.Height * Data.TileStepHeight;

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
        var r = w * Data.PathWidth;
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

        var roadColor = Data.PathColor;
        _builder.SetColor(_tile.HasPath ? roadColor : grassColor);
        _builder.AddQuad(r0, r1, r2, r3);

        _builder.SetColor(GetEdgeColor(_tile.WestEdge.Type));
        _builder.AddQuad(p45r0, p45r1, r1, r0);

        _builder.SetColor(GetEdgeColor(_tile.NorthEdge.Type));
        _builder.AddQuad(p56r1, p56r2, r2, r1);

        _builder.SetColor(GetEdgeColor(_tile.EastEdge.Type));
        _builder.AddQuad(p67r2, p67r3, r3, r2);

        _builder.SetColor(GetEdgeColor(_tile.SouthEdge.Type));
        _builder.AddQuad(p74r3, p74r0, r0, r3);

        GenerateWallMeshes(p4, p5, p6, p7);

        var result = new MeshGeneratorResult();
        result.Meshes.Add(_builder.BuildMesh());
        return result;
    }

    void GenerateWallMeshes(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        _builder.SetColor(Data.WallColor);
        if (_tile.NorthEdge.Type == EMapTileEdgeType.Wall)
        {
            AddWallMesh(p1, p2);
        }

        if (_tile.SouthEdge.Type == EMapTileEdgeType.Wall)
        {
            AddWallMesh(p3, p0);
        }

        if (_tile.WestEdge.Type == EMapTileEdgeType.Wall)
        {
            AddWallMesh(p0, p1);
        }

        if (_tile.EastEdge.Type == EMapTileEdgeType.Wall)
        {
            AddWallMesh(p2, p3);
        }
    }

    void AddWallMesh(Vector3 p0, Vector3 p1)
    {
        var wallInset = Vector3.Cross(p0 - p1, Vector3.up).normalized * Data.WallInset;

        var i0 = p0 + wallInset;
        var i1 = p1 + wallInset;
        var w0 = p0 + .5f * wallInset + Vector3.up * Data.WallInset;
        var w1 = p1 + .5f * wallInset + Vector3.up * Data.WallInset;
        var w2 = p0 + .5f * wallInset + Vector3.up * (1 - Data.WallInset);
        var w3 = p1 + .5f * wallInset + Vector3.up * (1 - Data.WallInset);
        var i2 = p0 + wallInset + Vector3.up;
        var i3 = p1 + wallInset + Vector3.up;
        var p2 = p0 + Vector3.up;
        var p3 = p1 + Vector3.up;

        _builder.AddQuad(i0, w0, w1, i1);
        _builder.AddQuad(w0, w2, w3, w1);
        _builder.AddQuad(i2, i3, w3, w2);
        _builder.AddQuad(i2, p2, p3, i3);
    }

    protected override TileMeshGeneratorData LoadData() => DataService.GetData<MeshGeneratorDataCollection>().Tile;

    Color GetEdgeColor(EMapTileEdgeType edge)
    {
        return edge switch
        {
            EMapTileEdgeType.Path => Data.PathColor,
            EMapTileEdgeType _ => Data.GrassColor,
        };
    }
}
