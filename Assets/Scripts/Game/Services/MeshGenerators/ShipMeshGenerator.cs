using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator;
using System;

[MeshGenerator("Ship")]
public class ShipMeshGenerator : MeshGeneratorWithData<ShipMeshGeneratorData>
{
    protected override void BuildMesh(MeshBuilder builder)
    {

    }

    protected override ShipMeshGeneratorData LoadData() => DataService.GetData<MeshGeneratorDataCollection>().Ship;
}
