using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator;
using System;
using MeshGenerator.Wireframe;

[MeshGenerator("Gear")]
public class GearMeshGenerator : MeshGeneratorWithData<GearMeshGeneratorData>
{
    protected override MeshGeneratorResult BuildMesh()
    {
        return new();
    }

    protected override GearMeshGeneratorData LoadData() => DataService.GetData<MeshGeneratorDataCollection>().Gear;
}
