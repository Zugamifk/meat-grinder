using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator;
using System;
using MeshGenerator.Wireframes;

[MeshGenerator("Gun Turret")]
public class GunTurretMeshGenerator : MeshGeneratorWithData<GunTurretMeshGeneratorData>
{
    protected override void BuildMesh(MeshBuilder builder)
    {
        var result = new MeshGeneratorResult();

        float w = Data.BaseDimensions.x / 2;
        float h = Data.BaseDimensions.y / 2;

        var b0 = new Vector3(-w, 0, -w);
        var b1 = new Vector3(-w, 0, w);
        var b2 = new Vector3(w, 0, w);
        var b3 = new Vector3(w, 0, -w);

        var mp = new Vector3(0, h, 0);

        // base 
        builder.AddTriangle(b0,b1,mp);
        builder.AddTriangle(b1,b2,mp);
        builder.AddTriangle(b2,b3,mp);
        builder.AddTriangle(b3,b0,mp);

        // receiver
        builder.SetBone(1);
        var mountedMatrix = GunTransform();
        builder.PushMatrix(mountedMatrix);
        builder.AddAxisAlignedBox(Data.GunBounds);

        // barrel
        builder.SetBone(2);
        builder.AddPrism(
            new Vector3(0,0,Data.GunBounds.z),
            Data.BarrelDimensions.x,
            12,
            Vector3.forward,
            Data.BarrelDimensions.y);
    }

    protected override MeshGeneratorResult BuildResult()
    {
        var result =  base.BuildResult();
        var mountedMatrix = GunTransform();
        var barrelEnd = mountedMatrix.MultiplyPoint3x4(new Vector3(0,0, Data.GunBounds.z + Data.BarrelDimensions.y));
        result.SpecialBones["BarrelEnd"] = barrelEnd;
        return result;
    }

    Matrix4x4 GunTransform()
    {
        return Matrix4x4.TRS(
            Data.MountPosition + new Vector3(0, Data.BaseDimensions.y / 2 + Data.GunBounds.y),
            Quaternion.AngleAxis(Data.MountedAngle, Vector3.up),
            Vector3.one);
    }

    protected override GunTurretMeshGeneratorData LoadData() => DataService.GetData<MeshGeneratorDataCollection>().GunTurret;
}
