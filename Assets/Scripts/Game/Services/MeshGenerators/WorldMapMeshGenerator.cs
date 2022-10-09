using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator;
using System;
using MeshGenerator.Wireframe;

[MeshGenerator("WorldMap")]
public class WorldMapMeshGenerator : MeshGeneratorWithData<WorldMapMeshGeneratorData>
{
    protected override MeshGeneratorResult BuildMesh()
    {
        return new();
    }

    protected override WorldMapMeshGeneratorData LoadData() => DataService.GetData<MeshGeneratorDataCollection>().WorldMap;
}
