using MeshGenerator;
using MeshGenerator.Wireframe;
using System;
using UnityEngine;

[MeshGenerator("Tile")]
public class TileMeshGenerator : MeshGeneratorWithWireFrame<TileMeshGeneratorData>
{
    ITileModel _tile;

    public void SetTile(ITileModel tile)
    {
        _tile = tile;
    }

    public override void BuildWireframe()
    {
        Wireframe = new();
        Func<float> h = () => _tile.Height * Data.TileStepHeight;

        var p0 = new Point(-.5f, h(), -.5f);
        var p1 = new Point(-.5f, h(), .5f);
        var p2 = new Point(.5f, h(), .5f);
        var p3 = new Point(.5f, h(), -.5f);
        Wireframe.ConnectLoop(p0, p1, p2, p3);

        if (_tile.HasPath)
        {
            GeneratePaths();
        }
    }

    void GeneratePaths()
    {
        Func<float> r = () => Data.PathWidth * .5f;
        Func<float> h = () => _tile.Height * Data.TileStepHeight;
        var r0 = new DynamicPoint(() => new Vector3(-r(), h(), -r()));
        var r1 = new DynamicPoint(() => new Vector3(-r(), h(), r()));
        var r2 = new DynamicPoint(() => new Vector3(r(), h(), r()));
        var r3 = new DynamicPoint(() => new Vector3(r(), h(), -r()));

        if (_tile.NorthEdge.Type == EMapTileEdgeType.Path)
        {
            var p0 = new DynamicPoint(() => new Vector3(-r(), h(), .5f));
            var p1 = new DynamicPoint(() => new Vector3(r(), h(), .5f));
            Wireframe.Connect(p0, r1);
            Wireframe.Connect(p1, r2);
        } else
        {
            Wireframe.Connect(r1, r2);
        }

        if (_tile.SouthEdge.Type == EMapTileEdgeType.Path)
        {
            var p0 = new DynamicPoint(() => new Vector3(-r(), h(), -.5f));
            var p1 = new DynamicPoint(() => new Vector3(r(), h(), -.5f));
            Wireframe.Connect(p0, r0);
            Wireframe.Connect(p1, r3);
        }
        else
        {
            Wireframe.Connect(r0, r3);
        }

        if (_tile.EastEdge.Type == EMapTileEdgeType.Path)
        {
            var p0 = new DynamicPoint(() => new Vector3(.5f, h(), -r()));
            var p1 = new DynamicPoint(() => new Vector3(.5f, h(), r()));
            Wireframe.Connect(p0, r3);
            Wireframe.Connect(p1, r2);
        }
        else
        {
            Wireframe.Connect(r3, r2);
        }

        if (_tile.WestEdge.Type == EMapTileEdgeType.Path)
        {
            var p0 = new DynamicPoint(() => new Vector3(-.5f, h(), r()));
            var p1 = new DynamicPoint(() => new Vector3(-.5f, h(), -r()));
            Wireframe.Connect(p0, r1);
            Wireframe.Connect(p1, r0);
        }
        else
        {
            Wireframe.Connect(r1, r0);
        }
    }

    public override void Generate(MeshBuilder builder)
    {
        var grassColor = Data.GrassColor;
        builder.SetColor(grassColor);

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

        builder.AddQuad(p0, p3, p2, p1);
        builder.AddQuad(p0, p4, p7, p3);
        builder.AddQuad(p0, p1, p5, p4);
        builder.AddQuad(p1, p2, p6, p5);
        builder.AddQuad(p3, p7, p6, p2);

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

        builder.AddQuad(p4, p45r0, r0, p74r0);
        builder.AddQuad(p5, p56r1, r1, p45r1);
        builder.AddQuad(p6, p67r2, r2, p56r2);
        builder.AddQuad(p7, p74r3, r3, p67r3);

        var roadColor = Data.PathColor;
        builder.SetColor(_tile.HasPath ? roadColor : grassColor);
        builder.AddQuad(r0, r1, r2, r3);

        builder.SetColor(GetEdgeColor(_tile.WestEdge.Type));
        builder.AddQuad(p45r0, p45r1, r1, r0);

        builder.SetColor(GetEdgeColor(_tile.NorthEdge.Type));
        builder.AddQuad(p56r1, p56r2, r2, r1);

        builder.SetColor(GetEdgeColor(_tile.EastEdge.Type));
        builder.AddQuad(p67r2, p67r3, r3, r2);

        builder.SetColor(GetEdgeColor(_tile.SouthEdge.Type));
        builder.AddQuad(p74r3, p74r0, r0, r3);
    }

    protected override TileMeshGeneratorData LoadData() => DataService.GetData<MeshGeneratorDataCollection>().Tile;

    Color GetEdgeColor(EMapTileEdgeType edge)
    {
        return edge switch
        {
            EMapTileEdgeType.Wall => Data.WallColor,
            EMapTileEdgeType.Path => Data.PathColor,
            EMapTileEdgeType _ => Data.GrassColor,
        };
    }
}
