using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator.Editor;
using MeshGenerator;

[MeshGeneratorEditor(typeof(GearAssemblyMeshGenerator))]
public class GearAssemblyMeshGeneratorEditor : MeshGeneratorEditorWithWireFrame<GearAssemblyMeshGenerator, GearAssemblyMeshGeneratorData>
{
    protected override void BuildWireframe()
    {
        var gear1 = new GearAssemblyMeshGenerator();

    }
}
