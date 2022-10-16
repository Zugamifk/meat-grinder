using MeshGenerator.Editor;
using MeshGenerator.Wireframes;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GunTurretWireframeGenerator : WireframeGenerator<GunTurretMeshGeneratorData>
{
    protected override void BuildWireframe(Wireframe wireframe, GunTurretMeshGeneratorData data)
    {
        float w = data.BaseDimensions.x / 2;
        float h = data.BaseDimensions.y / 2;

        var b0 = new Point(new Vector3(-w, 0, -w));
        var b1 = new Point(new Vector3(-w, 0, w));
        var b2 = new Point(new Vector3(w, 0, w));
        var b3 = new Point(new Vector3(w, 0, -w));

        var mp = new DynamicPoint(() => new Vector3(0, h, 0));

        wireframe.ConnectLoop(b0, b1, b2, b3);
        wireframe.Connect(b0, mp);
        wireframe.Connect(b1, mp);
        wireframe.Connect(b2, mp);
        wireframe.Connect(b3, mp);

        var gunMatrix = Matrix4x4.TRS(
            data.MountPosition + new Vector3(0, h + data.GunBounds.y),
            Quaternion.AngleAxis(data.MountedAngle, Vector3.up),
            Vector3.one);
        wireframe.Cuboid(() => gunMatrix, () => data.GunBounds.x, () => data.GunBounds.y, () => data.GunBounds.z);

        Vector3 barrel = gunMatrix.MultiplyPoint(new Vector3(0, 0, data.GunBounds.z));
        wireframe.Cylinder(() => barrel,
            () => data.BarrelDimensions.x,
            () => data.BarrelDimensions.y,
            () => gunMatrix.MultiplyVector(Vector3.forward),
            () => SceneView.currentDrawingSceneView.camera.transform.forward);
    }
}
