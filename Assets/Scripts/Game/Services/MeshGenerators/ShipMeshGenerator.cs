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
        builder.SetColor(Color.green);
        builder.AddAxisAlignedBox(new Vector3(1, 1, 3));
        builder.PushMatrix(Matrix4x4.Translate(Vector3.forward * 2));
        builder.SetColor(Color.red);
        builder.AddAxisAlignedBox(Vector3.one*.3f);
    }

    protected override ShipMeshGeneratorData LoadData() => DataService.GetData<MeshGeneratorDataCollection>().Ship;
}
