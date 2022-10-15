using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator.Editor;
using MeshGenerator;
using MeshGenerator.Wireframes;
using System;
using UnityEditor;

[MeshGeneratorEditor(typeof(GunTurretMeshGenerator))]
public class GunTurretMeshGeneratorEditor : MeshGeneratorEditorWithWireFrame<GunTurretMeshGenerator, GunTurretMeshGeneratorData>
{
    protected override void BuildWireframe()
    {
        Func<float> w = () => _data.BaseDimensions.x / 2;
        Func<float> h = () => _data.BaseDimensions.y / 2;

        var b0 = new DynamicPoint(() => new Vector3(-w(), 0, -w()));
        var b1 = new DynamicPoint(() => new Vector3(-w(), 0, w()));
        var b2 = new DynamicPoint(() => new Vector3(w(), 0, w()));
        var b3 = new DynamicPoint(() => new Vector3(w(), 0, -w()));

        var mp = new DynamicPoint(() => new Vector3(0, h(), 0));

        _wireframe.ConnectLoop(b0, b1, b2, b3);
        _wireframe.Connect(b0, mp);
        _wireframe.Connect(b1, mp);
        _wireframe.Connect(b2, mp);
        _wireframe.Connect(b3, mp);

        Func<Matrix4x4> gunMatrix = () => Matrix4x4.TRS(
            _data.MountPosition + new Vector3(0, h() + _data.GunBounds.y),
            Quaternion.AngleAxis(_data.MountedAngle, Vector3.up),
            Vector3.one);
        _wireframe.Cuboid(gunMatrix, () => _data.GunBounds.x, () => _data.GunBounds.y, () => _data.GunBounds.z);

        Func<Vector3> barrel = () => gunMatrix().MultiplyPoint(new Vector3(0, 0, _data.GunBounds.z));
        _wireframe.Cylinder(barrel, 
            () => _data.BarrelDimensions.x, 
            () => _data.BarrelDimensions.y, 
            () => gunMatrix().MultiplyVector(Vector3.forward),
            ()=>SceneView.currentDrawingSceneView.camera.transform.forward);
    }
}
