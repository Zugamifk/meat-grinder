using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator.Editor;
using MeshGenerator;

[MeshGeneratorEditor(typeof(GearMeshGenerator))]
public class GearMeshGeneratorEditor : MeshGeneratorEditorWithWireFrame<GearMeshGenerator, GearMeshGeneratorData>
{
    public override void BuildWireframe()
    {

    }
}
