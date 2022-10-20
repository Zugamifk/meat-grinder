using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator;
using System;
using MeshGenerator.Wireframes;

[MeshGenerator("GearAssembly")]
public class GearAssemblyMeshGenerator : MeshGeneratorWithData<GearAssemblyMeshGeneratorData>
{
    GearAssemblyMeshGeneratorData _customData;


    public GearAssemblyMeshGenerator(GearAssemblyMeshGeneratorData customData)
    {
        _customData = customData;
    }

    public GearAssemblyMeshGenerator() { }

    protected override void BuildMesh(MeshBuilder builder)
    {
    }

    protected override GearAssemblyMeshGeneratorData LoadData() => _customData ?? DataService.GetData<MeshGeneratorDataCollection>().GearAssembly;
}
