using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator;
using System;
using MeshGenerator.Wireframe;

[MeshGenerator("Gun Turret")]
public class GunTurretMeshGenerator : MeshGeneratorWithWireFrame<GunTurretMeshGeneratorData>
{
    public override void BuildWireframe()
    {
        Wireframe = new();

        Func<float> w = () => Data.BaseDimensions.x / 2;
        Func<float> h = () => Data.BaseDimensions.y / 2;

        var b0 = new DynamicPoint(() => new Vector3(-w(), 0, -w()));
        var b1 = new DynamicPoint(() => new Vector3(-w(), 0, w()));
        var b2 = new DynamicPoint(() => new Vector3(w(), 0, w()));
        var b3 = new DynamicPoint(() => new Vector3(w(), 0, -w()));

        var mp = new DynamicPoint(() => new Vector3(0, h(), 0));

        Wireframe.ConnectLoop(b0, b1, b2, b3);
        Wireframe.Connect(b0, mp);
        Wireframe.Connect(b1, mp);
        Wireframe.Connect(b2, mp);
        Wireframe.Connect(b3, mp);

        Func<Matrix4x4> gunMatrix = () => Matrix4x4.TRS(
            Data.MountPosition + new Vector3(0,  h() + Data.GunBounds.y),
            Quaternion.AngleAxis(Data.MountedAngle, Vector3.up),
            Vector3.one);
        Wireframe.Cuboid(gunMatrix, () => Data.GunBounds.x, () => Data.GunBounds.y, () => Data.GunBounds.z);

        Func<Vector3> barrel = () => gunMatrix().MultiplyPoint(new Vector3(0, 0, Data.GunBounds.z));
        Wireframe.Cylinder(barrel, ()=>Data.BarrelDimensions.x, ()=>Data.BarrelDimensions.y, ()=>gunMatrix().MultiplyVector(Vector3.forward));
    }

    public override MeshGeneratorResult Generate()
    {
        throw new System.NotImplementedException();
    }

    protected override GunTurretMeshGeneratorData LoadData() => DataService.GetData<MeshGeneratorDataCollection>().GunTurret;
}
