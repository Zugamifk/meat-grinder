using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator;
using MeshGenerator.Editor;
using UnityEditor;
using MeshGenerator.Wireframes;
using System;

[MeshGeneratorEditor(typeof(TileMeshGenerator))]
public class TileMeshGeneratorEditor : MeshGeneratorEditorWithWireFrame<TileMeshGenerator, TileWireframeGenerator, TileMeshGeneratorData>
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
        _wireframeGenerator.SetTile(_tile);
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
            _wireframeGenerator.SetTile(_tile);
            RebuildWireframe();
        }
    }

    
}
