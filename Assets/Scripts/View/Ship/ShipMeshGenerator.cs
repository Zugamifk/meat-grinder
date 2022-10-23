using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator;
using System;

[MeshGenerator("Ship")]
public class ShipMeshGenerator : ModelMeshGenerator<IShipModel, ShipMeshGeneratorData>
{
    protected override void BuildMesh(MeshBuilder builder)
    {
        var color = Model.IsFriend ? Color.green : Color.red;
        builder.SetColor(color);
        builder.AddAxisAlignedBox(new Vector3(1, 1, 3));
        builder.PushMatrix(Matrix4x4.Translate(Vector3.forward * 2));
        builder.SetColor(Color.blue);
        builder.AddAxisAlignedBox(Vector3.one*.3f);
    }

    protected override ShipMeshGeneratorData LoadData() => DataService.GetData<MeshGeneratorDataCollection>().Ship;
}
