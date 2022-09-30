using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator;
using System;
using MeshGenerator.Wireframe;

[MeshGenerator("Gun Turret")]
public class GunTurretMeshGenerator : MeshGeneratorWithData<GunTurretMeshGeneratorData>
{
    public override MeshGeneratorResult Generate()
    {
        var result = new MeshGeneratorResult();

        float w = Data.BaseDimensions.x / 2;
        float h = Data.BaseDimensions.y / 2;

        var b0 = new Vector3(-w, 0, -w);
        var b1 = new Vector3(-w, 0, w);
        var b2 = new Vector3(w, 0, w);
        var b3 = new Vector3(w, 0, -w);

        var mp = new Vector3(0, h, 0);

        _builder.AddTriangle(b0,b1,mp);
        _builder.AddTriangle(b1,b2,mp);
        _builder.AddTriangle(b2,b3,mp);
        _builder.AddTriangle(b3,b0,mp);

        result.Meshes.Add(_builder.BuildMesh());
        _builder.Clear();

        return result;
    }

    protected override GunTurretMeshGeneratorData LoadData() => DataService.GetData<MeshGeneratorDataCollection>().GunTurret;
}
