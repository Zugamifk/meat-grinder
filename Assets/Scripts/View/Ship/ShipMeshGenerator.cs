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
        var segmentLength = Data.BalloonRingCount;
        var balloonLength = Data.BalloonLength;
        var numSegments = Data.BalloonRingSides;
        var balloonRadius = Data.BalloonRadius;
        var balloonCurve = Data.BalloonCurve;
        var step = Mathf.Max(0.05f, segmentLength) / balloonLength;
        var dir = Vector3.up;
        for (float t = 0; t < 1; t += step)
        {
            var next = Mathf.Min(t + step, 1);
            var c0 = Vector3.forward * t * balloonLength;
            var c1 = Vector3.forward * next * balloonLength;
            var r0 = balloonCurve.Evaluate(t) * balloonRadius;
            var r1 = balloonCurve.Evaluate(next) * balloonRadius;
            CreateBalloonSegment(builder, dir, c0, c1, r0, r1, numSegments);
        }
        var start = Vector3.zero;
        CreateBalloonSegment(builder, dir, start, start, balloonCurve.Evaluate(0) * balloonRadius, 0, numSegments);
        var end = Vector3.forward * balloonLength;
        CreateBalloonSegment(builder, dir, end, end, balloonCurve.Evaluate(1) * balloonRadius, 0, numSegments);
    }

    void CreateBalloonSegment(MeshBuilder builder, Vector3 dir, Vector3 c0, Vector3 c1, float r0, float r1, int sides)
    {
        var d = dir;
        var ang = 360 / (float)sides;
        var rot = Quaternion.AngleAxis(ang, Vector3.forward);
        for (int i = 0; i < sides; i++)
        {
            var p0 = c0 + rot * d * r0;
            var p1 = c1 + rot * d * r1;
            var p2 = c1 + d * r1;
            var p3 = c0 + d * r0;
            builder.AddQuad(p0, p1, p2, p3);
            d = rot * d;
        }
    }

    void CreateStabilizer(MeshBuilder builder, Vector3 dir)
    {
        var x0 = Data.StabilizerPosition.x;
        var x1 = Data.StabilizerPosition.y;
        var w = Data.StabilizerThickness;
        var step = Mathf.Max(1 / (float)Data.StabilizerSegments, 0.05f);
        var r = Data.BalloonCurve.Evaluate(x0) * Data.BalloonRadius;
        for (float t = 0; t < 1; t += step)
        {
            var t0 = Mathf.Lerp(x0, x1, t);
            var t1 = Mathf.Lerp(x0, x1, t + step);
            var c0 = Vector3.forward * t0 * Data.BalloonLength;
            var c1 = Vector3.forward * t1 * Data.BalloonLength;
            
            CreateStabilizerSegment(builder, c0, c1, dir, r, t, step, w / 2);
        }
        var c = Vector3.forward * x1 * Data.BalloonLength;
        //CreateStabilizerSegment(builder, c, c, dir, r + Data.StabilizerCurve.Evaluate(1) * l, Data.BalloonCurve.Evaluate(x1) * Data.BalloonRadius, w / 2);
    }

    void CreateStabilizerSegment(MeshBuilder builder, Vector3 c0, Vector3 c1, Vector3 dir, float r, float t, float step, float w)
    {
        var l = Data.StabilizerLength;
        var offset = Vector3.Cross(Vector3.forward, dir).normalized * w;
        var l0 = r + Data.StabilizerCurve.Evaluate(t) * l;
        var l1 = r + Data.StabilizerCurve.Evaluate(t + step) * l;
        var r0 = Data.BalloonCurve.Evaluate(Mathf.Lerp(Data.StabilizerPosition.x, Data.StabilizerPosition.y, t));
        var r1 = Data.BalloonCurve.Evaluate(Mathf.Lerp(Data.StabilizerPosition.x, Data.StabilizerPosition.y, t+step));
        var p0 = c0 + dir * r0;
        var p1 = c1 + dir * r1;
        var p2 = c0 + dir * l0;
        var p3 = c1 + dir * l1;
        builder.AddQuad(p1-offset, p0 - offset, p2 - offset, p3 - offset);
        builder.AddQuad(p0 + offset, p1 + offset, p3 + offset, p2 + offset);
        builder.AddQuad(p2 + offset, p3 + offset, p3 - offset, p2 - offset);
    }

    protected override ShipMeshGeneratorData LoadData() => DataService.GetData<MeshGeneratorDataCollection>().Ship;
}
