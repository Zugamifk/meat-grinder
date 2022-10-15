using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator;
using System;
using MeshGenerator.Wireframe;

[MeshGenerator("GearAssembly")]
public class GearAssemblyMeshGenerator : MeshGeneratorWithData<GearAssemblyMeshGeneratorData>
{
    protected override void BuildMesh()
    {
    }

    protected override GearAssemblyMeshGeneratorData LoadData() => DataService.GetData<MeshGeneratorDataCollection>().GearAssembly;
}
