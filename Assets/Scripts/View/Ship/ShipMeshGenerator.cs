using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator;
using System;
using MeshGenerator.Wireframes;
using MeshGenerator.Surfaces;

[MeshGenerator("Ship")]
public class ShipMeshGenerator : ModelMeshGenerator<IShipModel, ShipMeshGeneratorData>
{
    protected override void BuildMesh(MeshBuilder builder)
    {
        //CreateDebugMesh(builder);
        CreateBalloon(builder);
        CreateStabilizer(builder, Vector3.up);
        CreateStabilizer(builder, Vector3.left);
        CreateStabilizer(builder, Vector3.right);
        CreateGondola(builder);
    }

    void CreateDebugMesh(MeshBuilder builder)
    {
        var color = Model.IsFriend ? Color.green : Color.red;
        builder.SetColor(color);
        builder.AddAxisAlignedBox(new Vector3(1, 1, 3));
        builder.PushMatrix(Matrix4x4.Translate(Vector3.forward * 2));
        builder.SetColor(Color.blue);
        builder.AddAxisAlignedBox(Vector3.one * .3f);
    }

    void CreateBalloon(MeshBuilder builder)
    {
        for (int i = 0; i < Data.BalloonRingCount; i++)
        {
            for (int j = 0; j < Data.BalloonRingSides; j++)
            {
                var u0 = (float)i / Data.BalloonRingCount;
                var u1 = (float)(i + 1) / Data.BalloonRingCount;
                var v0 = (float)j / Data.BalloonRingSides;
                var v1 = (float)(j + 1) / Data.BalloonRingSides;
                var p0 = Data.GetBalloonPoint(u0, 360 * v0);
                var p1 = Data.GetBalloonPoint(u0, 360 * v1);
                var p2 = Data.GetBalloonPoint(u1, 360 * v1);
                var p3 = Data.GetBalloonPoint(u1, 360 * v0);
                builder.AddQuad(p0, p1, p2, p3);
            }
        }
    }

    void CreateStabilizer(MeshBuilder builder, Vector3 dir)
    {
        var offset = Vector3.Cross(dir, Vector3.forward).normalized * Data.StabilizerThickness / 2;
        for (int i = 0; i < Data.StabilizerSegments; i++)
        {
            var u0 = (float)i / Data.StabilizerSegments;
            var e0 = Data.GetStabilizerEdgePoint(u0, dir);
            var b0 = Data.GetBalloonPoint(Mathf.Lerp(Data.StabilizerPosition.x, Data.StabilizerPosition.y, u0), dir);
            var u1 = (float)(i + 1) / Data.StabilizerSegments;
            var e1 = Data.GetStabilizerEdgePoint(u1, dir);
            var b1 = Data.GetBalloonPoint(Mathf.Lerp(Data.StabilizerPosition.x, Data.StabilizerPosition.y, u1), dir);
            builder.AddQuad(b0 + offset, e0 + offset, e1 + offset, b1 + offset);
            builder.AddQuad(b0 - offset, b1 - offset, e1 - offset, e0 - offset);
            builder.AddQuad(e0 + offset, e0 - offset, e1 - offset, e1 + offset);
        }
    }

    void CreateGondola(MeshBuilder builder)
    {
        var mtx = Matrix4x4.Translate(Data.GondolaPosition);
        builder.PushMatrix(mtx);
        var zStep = Mathf.Max(0.05f, 1 / (float)Data.GondolaSegments);
        var yStep = Mathf.Max(0.05f, 1 / (float)Data.GondolaLayers);
        for (float t = 0; t < 1; t += zStep)
        {
            for (float y = 0; y < 1; y += yStep)
            {
                Vector3 l0, l1, l2, l3;
                var r0 = Data.GetGondolaPoint(t, y, out l0);
                var r1 = Data.GetGondolaPoint(t + zStep, y, out l1);
                var r2 = Data.GetGondolaPoint(t + zStep, y + yStep, out l2);
                var r3 = Data.GetGondolaPoint(t, y + yStep, out l3);

                builder.AddQuad(r2, r1, r0, r3);
                builder.AddQuad(l0, l1, l2, l3);
            }
        }
        builder.PopMatrix();
    }

    protected override ShipMeshGeneratorData LoadData() => DataService.GetData<MeshGeneratorDataCollection>().Ship;
}
