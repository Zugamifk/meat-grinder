using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator;
using MeshGenerator.Editor;
using UnityEditor;
using MeshGenerator.Wireframes;
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
            Type = ETileType.Empty,
        };
        _tile.NorthEdge = new() { Tile = _tile, Type = ETileType.Empty };
        _tile.SouthEdge = new() { Tile = _tile, Type = ETileType.Empty };
        _tile.EastEdge = new() { Tile = _tile, Type = ETileType.Empty };
        _tile.WestEdge = new() { Tile = _tile, Type = ETileType.Empty };

        _generator.SetTile(_tile);
    }

    protected override void DrawInspectorFields()
    {
        EditorGUI.BeginChangeCheck();

        _tile.Type = (ETileType)EditorGUILayout.EnumPopup("Tile Type", _tile.Type);
        _tile.NorthEdge.Type = (ETileType)EditorGUILayout.EnumPopup("North Edge", _tile.NorthEdge.Type);
        _tile.SouthEdge.Type = (ETileType)EditorGUILayout.EnumPopup("South Edge", _tile.SouthEdge.Type);
        _tile.EastEdge.Type = (ETileType)EditorGUILayout.EnumPopup("East Edge", _tile.EastEdge.Type);
        _tile.WestEdge.Type = (ETileType)EditorGUILayout.EnumPopup("West Edge", _tile.WestEdge.Type);

        if (EditorGUI.EndChangeCheck())
        {
            _generator.SetTile(_tile);
            RebuildWireframe();
        }
    }

    protected override void BuildWireframe()
    {
        Func<float> h = () => _tile.Height * _data.TileStepHeight;

        var p0 = new Point(-.5f, h(), -.5f);
        var p1 = new Point(-.5f, h(), .5f);
        var p2 = new Point(.5f, h(), .5f);
        var p3 = new Point(.5f, h(), -.5f);
        _wireframe.ConnectLoop(p0, p1, p2, p3);

        if (_tile.Type==ETileType.Path)
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

        if (_tile.NorthEdge.Type == ETileType.Path)
        {
            var p0 = new DynamicPoint(() => new Vector3(-r(), h(), .5f));
            var p1 = new DynamicPoint(() => new Vector3(r(), h(), .5f));
            _wireframe.Connect(p0, r1);
            _wireframe.Connect(p1, r2);
        }
        else
        {
            _wireframe.Connect(r1, r2);
        }

        if (_tile.SouthEdge.Type == ETileType.Path)
        {
            var p0 = new DynamicPoint(() => new Vector3(-r(), h(), -.5f));
            var p1 = new DynamicPoint(() => new Vector3(r(), h(), -.5f));
            _wireframe.Connect(p0, r0);
            _wireframe.Connect(p1, r3);
        }
        else
        {
            _wireframe.Connect(r0, r3);
        }

        if (_tile.EastEdge.Type == ETileType.Path)
        {
            var p0 = new DynamicPoint(() => new Vector3(.5f, h(), -r()));
            var p1 = new DynamicPoint(() => new Vector3(.5f, h(), r()));
            _wireframe.Connect(p0, r3);
            _wireframe.Connect(p1, r2);
        }
        else
        {
            _wireframe.Connect(r3, r2);
        }

        if (_tile.WestEdge.Type == ETileType.Path)
        {
            var p0 = new DynamicPoint(() => new Vector3(-.5f, h(), r()));
            var p1 = new DynamicPoint(() => new Vector3(-.5f, h(), -r()));
            _wireframe.Connect(p0, r1);
            _wireframe.Connect(p1, r0);
        }
        else
        {
            _wireframe.Connect(r1, r0);
        }
    }

    private void GenerateWallWireFrames(IPoint p0, IPoint p1, IPoint p2, IPoint p3)
    {
        if (_tile.NorthEdge.Type == ETileType.Wall)
        {
            DrawWall(p1, p2);
        }

        if (_tile.SouthEdge.Type == ETileType.Wall)
        {
            DrawWall(p3, p0);
        }

        if (_tile.WestEdge.Type == ETileType.Wall)
        {
            DrawWall(p0, p1);
        }

        if (_tile.EastEdge.Type == ETileType.Wall)
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
        _wireframe.Connect(i0, i1);
        _wireframe.Connect(i0, w0);
        _wireframe.Connect(i1, w1);
        _wireframe.Connect(w0, w1);
        _wireframe.Connect(w0, w2);
        _wireframe.Connect(w1, w3);
        _wireframe.Connect(w2, w3);
        _wireframe.Connect(w2, i2);
        _wireframe.Connect(w3, i3);
        _wireframe.Connect(i2, i3);
        _wireframe.Connect(i2, new DynamicPoint(() => cornerA.Position + Vector3.up));
        _wireframe.Connect(i3, new DynamicPoint(() => cornerB.Position + Vector3.up));
    }
}
