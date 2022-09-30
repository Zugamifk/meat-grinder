using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator;
using MeshGenerator.Editor;
using UnityEditor;
using MeshGenerator.Wireframe;
using System;

[MeshGeneratorEditor(typeof(TileMeshGenerator))]
public class TileMeshGeneratorEditor : MeshGeneratorEditorWithWireFrame<TileMeshGenerator, TileMeshGeneratorData>
{
    TileModel _tile;

    protected override void OnSetGenerator()
    {
        _tile = new TileModel()
        {
            Height = 1,
            HasPath = false,
        };
        _tile.NorthEdge = new() { Tile = _tile, Type = EMapTileEdgeType.Empty };
        _tile.SouthEdge = new() { Tile = _tile, Type = EMapTileEdgeType.Empty };
        _tile.EastEdge = new() { Tile = _tile, Type = EMapTileEdgeType.Empty };
        _tile.WestEdge = new() { Tile = _tile, Type = EMapTileEdgeType.Empty };

        _generator.SetTile(_tile);
    }

    protected override void DrawInspectorFields()
    {
        EditorGUI.BeginChangeCheck();

        _tile.HasPath = EditorGUILayout.Toggle("Has Path", _tile.HasPath);
        _tile.NorthEdge.Type = (EMapTileEdgeType)EditorGUILayout.EnumPopup("North Edge", _tile.NorthEdge.Type);
        _tile.SouthEdge.Type = (EMapTileEdgeType)EditorGUILayout.EnumPopup("South Edge", _tile.SouthEdge.Type);
        _tile.EastEdge.Type = (EMapTileEdgeType)EditorGUILayout.EnumPopup("East Edge", _tile.EastEdge.Type);
        _tile.WestEdge.Type = (EMapTileEdgeType)EditorGUILayout.EnumPopup("West Edge", _tile.WestEdge.Type);

        if (EditorGUI.EndChangeCheck())
        {
            _generator.SetTile(_tile);
            BuildWireframe();
        }
    }

    public override void BuildWireframe()
    {
        Wireframe = new();
        Func<float> h = () => _tile.Height * _data.TileStepHeight;

        var p0 = new Point(-.5f, h(), -.5f);
        var p1 = new Point(-.5f, h(), .5f);
        var p2 = new Point(.5f, h(), .5f);
        var p3 = new Point(.5f, h(), -.5f);
        Wireframe.ConnectLoop(p0, p1, p2, p3);

        if (_tile.HasPath)
        {
            GeneratePaths();
        }

        GenerateWallWireFrames(p0, p1, p2, p3);
    }

    void GeneratePaths()
    {
        Func<float> r = () => _data.PathWidth * .5f;
        Func<float> h = () => _tile.Height * _data.TileStepHeight;
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
        }
        else
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

    private void GenerateWallWireFrames(IPoint p0, IPoint p1, IPoint p2, IPoint p3)
    {
        if (_tile.NorthEdge.Type == EMapTileEdgeType.Wall)
        {
            DrawWall(p1, p2);
        }

        if (_tile.SouthEdge.Type == EMapTileEdgeType.Wall)
        {
            DrawWall(p3, p0);
        }

        if (_tile.WestEdge.Type == EMapTileEdgeType.Wall)
        {
            DrawWall(p0, p1);
        }

        if (_tile.EastEdge.Type == EMapTileEdgeType.Wall)
        {
            DrawWall(p2, p3);
        }
    }

    void DrawWall(IPoint cornerA, IPoint cornerB)
    {
        Func<Vector3> wallInset = () => Vector3.Cross(cornerA.Position - cornerB.Position, Vector3.up).normalized * _data.WallInset;

        var i0 = new DynamicPoint(() => cornerA.Position + wallInset());
        var i1 = new DynamicPoint(() => cornerB.Position + wallInset());
        var w0 = new DynamicPoint(() => cornerA.Position + .5f * wallInset() + Vector3.up * _data.WallInset);
        var w1 = new DynamicPoint(() => cornerB.Position + .5f * wallInset() + Vector3.up * _data.WallInset);
        var w2 = new DynamicPoint(() => cornerA.Position + .5f * wallInset() + Vector3.up * (1 - _data.WallInset));
        var w3 = new DynamicPoint(() => cornerB.Position + .5f * wallInset() + Vector3.up * (1 - _data.WallInset));
        var i2 = new DynamicPoint(() => cornerA.Position + wallInset() + Vector3.up);
        var i3 = new DynamicPoint(() => cornerB.Position + wallInset() + Vector3.up);
        Wireframe.Connect(i0, i1);
        Wireframe.Connect(i0, w0);
        Wireframe.Connect(i1, w1);
        Wireframe.Connect(w0, w1);
        Wireframe.Connect(w0, w2);
        Wireframe.Connect(w1, w3);
        Wireframe.Connect(w2, w3);
        Wireframe.Connect(w2, i2);
        Wireframe.Connect(w3, i3);
        Wireframe.Connect(i2, i3);
        Wireframe.Connect(i2, new DynamicPoint(() => cornerA.Position + Vector3.up));
        Wireframe.Connect(i3, new DynamicPoint(() => cornerB.Position + Vector3.up));
    }
}
