using MeshGenerator.Editor;
using MeshGenerator.Wireframes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearAssemblyWireframeGenerator : WireframeGenerator<GearAssemblyMeshGeneratorData>
{
    GearWireframeGenerator _gearGenerator = new();

    protected override void BuildWireframe(Wireframe wireframe, GearAssemblyMeshGeneratorData data)
    {
        
    }
}
