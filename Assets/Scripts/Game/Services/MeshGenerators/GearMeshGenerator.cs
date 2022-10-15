using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator;
using System;
using MeshGenerator.Wireframes;

[MeshGenerator("Gear")]
public class GearMeshGenerator : MeshGeneratorWithData<GearMeshGeneratorData>
{
    protected override void BuildMesh()
    {
        var ang = 360f / (float)Data.TeethCount;
        var d = Vector3.right;
        var toothRot = Quaternion.Euler(0, 0, ang * Data.ToothThickness);
        var climbRot = Quaternion.Euler(0, 0, ang * (0.5f - Data.ToothThickness));
        // radius to inside tooth
        var r0 = Data.Radius;
        // radius to outside tooth
        var r1 = r0 + Data.WorkingDepth;
        // radius to axle
        var r2 = Data.AxelRadius;
        var oppStep = Vector3.forward * Data.GearThickness;
        for (int i = 0; i < Data.TeethCount; i++)
        {
            var p0 = d * r0;
            var t0 = d * r2;
            var t1 = t0 + oppStep;
            d = climbRot * d;
            var p1 = d * r1;
            d = toothRot * d;
            var p2 = d * r1;
            d = climbRot * d;
            var p3 = d * r0;
            var t3 = d * r2;
            var t2 = t3 + oppStep;
            d = toothRot * d;
            var p4 = d * r0;
            var t4 = d * r2;
            var t5 = t4 + oppStep;
            _builder.AddQuad(p2, p1, p0, p3);

            var p5 = p0 + oppStep;
            var p6 = p1 + oppStep;
            var p7 = p2 + oppStep;
            var p8 = p3 + oppStep;
            var p9 = p4 + oppStep;
            _builder.AddQuad(p5, p6, p7, p8);
            _builder.AddQuad(p0, p1, p6, p5);
            _builder.AddQuad(p1, p2, p7, p6);
            _builder.AddQuad(p2, p3, p8, p7);
            _builder.AddQuad(p3, p4, p9, p8);

            _builder.AddQuad(t0, t1, t2, t3);
            _builder.AddQuad(t4, t3, t2, t5);
            _builder.AddQuad(t0, t3, p3, p0);
            _builder.AddQuad(p4, p3, t3, t4);
            _builder.AddQuad(p8, t2, t1, p5);
            _builder.AddQuad(t2, p8, p9, t5);
        }
    }

    protected override GearMeshGeneratorData LoadData() => DataService.GetData<MeshGeneratorDataCollection>().Gear;
}
