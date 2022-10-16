using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator.Editor;
using MeshGenerator;
using MeshGenerator.Wireframes;
using System;
using System.Runtime.InteropServices;

[MeshGeneratorEditor(typeof(WorldMapMeshGenerator))]
public class WorldMapMeshGeneratorEditor : MeshGeneratorEditorWithWireFrame<WorldMapMeshGenerator, WorldMapWireframeGenerator, WorldMapMeshGeneratorData>
{
    protected override void OnPropertiesChanged()
    {
        _generator.GenerateOffsets();
    }

    protected override void OnSetGenerator()
    {
        _generator.GenerateOffsets();
    }
}
