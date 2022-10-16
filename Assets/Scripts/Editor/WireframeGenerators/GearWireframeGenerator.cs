using MeshGenerator.Editor;
using MeshGenerator.Wireframes;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GearWireframeGenerator : WireframeGenerator<GearMeshGeneratorData>
{
    protected override void BuildWireframe(Wireframe wireframe, GearMeshGeneratorData data)
    {
        var ang = 360f / (float)data.TeethCount;
        var d = Vector3.right;
        var toothRot = Quaternion.Euler(0, 0, ang * data.ToothThickness);
        var climbRot = Quaternion.Euler(0, 0, ang * (0.5f - data.ToothThickness));
        var r0 = data.Radius;
        var r1 = r0 + data.WorkingDepth;
        var points = new List<IPoint>();
        for (int i = 0; i < data.TeethCount; i++)
        {
            var d0 = d;
            var d1 = climbRot * d0;
            var d2 = toothRot * d1;
            var d3 = climbRot * d2;
            var d4 = toothRot * d3;
            points.Add(new Point(d0 * r0));
            points.Add(new Point(d1 * r1));
            points.Add(new Point(d2 * r1));
            points.Add(new Point(d3 * r0));
            d = d4;
        }
        wireframe.Prism(points, () => Vector3.forward, () => data.GearThickness);
        wireframe.Cylinder(() => Vector3.zero, () => data.AxelRadius, () => data.GearThickness, () => Vector3.forward, () => SceneView.currentDrawingSceneView.camera.transform.forward);
    }
}
