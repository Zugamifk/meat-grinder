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
        float h = data.BaseDimensions.y;

        var b0 = new Vector3(-w, 0, -w);
        var b1 = new Vector3(-w, 0, w);
        var b2 = new Vector3(w, 0, w);
        var b3 = new Vector3(w, 0, -w);

        var mp = new Vector3(0, h, 0);

        wireframe.ConnectLoop(b0, b1, b2, b3);
        wireframe.Connect(b0, mp);
        wireframe.Connect(b1, mp);
        wireframe.Connect(b2, mp);
        wireframe.Connect(b3, mp);

        var gunMatrix = Matrix4x4.TRS(
            data.MountPosition + new Vector3(0, h + data.GunBounds.y/2),
            Quaternion.AngleAxis(data.MountedAngle, Vector3.up),
            Vector3.one);
        wireframe.PushMatrix(gunMatrix);
        wireframe.Cuboid(data.GunBounds.x, data.GunBounds.y, data.GunBounds.z);

        wireframe.Cylinder(new Vector3(0, 0, data.GunBounds.z/2),
            data.BarrelDimensions.x,
            data.BarrelDimensions.y,
            Vector3.forward);

        wireframe.PopMatrix();
    }
}
