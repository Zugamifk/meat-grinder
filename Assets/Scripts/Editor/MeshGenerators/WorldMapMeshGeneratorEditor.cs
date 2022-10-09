using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator.Editor;
using MeshGenerator;

[MeshGeneratorEditor(typeof(WorldMapMeshGenerator))]
public class WorldMapMeshGeneratorEditor : MeshGeneratorEditorWithWireFrame<WorldMapMeshGenerator, WorldMapMeshGeneratorData>
{
        public override void BuildWireframe()
    {

    }
}
