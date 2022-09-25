using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator;
using MeshGenerator.Editor;
using UnityEditor;

[MeshGeneratorEditor(typeof(TileMeshGenerator))]
public class TileMeshGeneratorEditor : MeshGeneratorWithWireFrameEditor<TileMeshGenerator, TileMeshGeneratorData>
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
            _generator.BuildWireframe();
        }
    }
}
