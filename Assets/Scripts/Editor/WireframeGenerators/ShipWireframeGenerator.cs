using MeshGenerator.Editor;
using MeshGenerator.Surfaces;
using MeshGenerator.Wireframes;
using PlasticGui.WebApi.Responses;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class ShipWireframeGenerator : WireframeGenerator<ShipMeshGeneratorData>
{

    protected override void BuildWireframe(Wireframe wireframe, ShipMeshGeneratorData data)
    {
        CreateBalloon(wireframe, data);
        CreateStabilizer(wireframe, data, Vector3.up);
        CreateStabilizer(wireframe, data, Vector3.left);
        CreateStabilizer(wireframe, data, Vector3.right);

        CreateGondola(wireframe, data);
    }

    void CreateBalloon(Wireframe wireframe, ShipMeshGeneratorData data)
    {
        var step = Mathf.Max(0.05f, data.SegmentLength) / data.BalloonLength;
        var sides = data.NumRidges;
        var dir = Vector3.up;
        for (float t = 0; t < 1; t += step)
        {
            var next = Mathf.Min(t + step, 1);
            var c0 = Vector3.forward * t * data.BalloonLength;
            var c1 = Vector3.forward * next * data.BalloonLength;
            var r0 = data.BalloonCurve.Evaluate(t) * data.BalloonRadius;
            var r1 = data.BalloonCurve.Evaluate(next) * data.BalloonRadius;
            CreateBalloonSegment(wireframe, dir, c0, c1, r0, r1, data.NumRidges);
        }
        var start = Vector3.zero;
        CreateBalloonSegment(wireframe, dir, start, start, data.BalloonCurve.Evaluate(0) * data.BalloonRadius, 0, data.NumRidges);
        var end = Vector3.forward * data.BalloonLength;
        CreateBalloonSegment(wireframe, dir, end, end, data.BalloonCurve.Evaluate(1) * data.BalloonRadius, 0, data.NumRidges);
    }

    void CreateBalloonSegment(Wireframe wireframe, Vector3 dir, Vector3 c0, Vector3 c1, float r0, float r1, int sides)
    {
        var d = dir;
        var ang = 360 / (float)sides;
        var rot = Quaternion.AngleAxis(ang, Vector3.forward);
        for (int i = 0; i < sides; i++)
        {
            var p0 = c0 + d * r0;
            var p1 = c1 + d * r1;
            wireframe.Connect(p0, p1);
            d = rot * d;
        }
    }

    void CreateStabilizer(Wireframe wireframe, ShipMeshGeneratorData data, Vector3 dir)
    {
        var x0 = data.StabilizerPosition.x;
        var x1 = data.StabilizerPosition.y;
        var l = data.StabilizerLength;
        var w = data.StabilizerThickness;
        var step = Mathf.Max(1 / (float)data.StabilizerSegments, 0.05f);
        var r = data.BalloonCurve.Evaluate(x0) * data.BalloonRadius;
        for (float t = 0; t < 1; t += step)
        {
            var t0 = Mathf.Lerp(x0, x1, t);
            var t1 = Mathf.Lerp(x0, x1, t + step);
            var c0 = Vector3.forward * t0 * data.BalloonLength;
            var c1 = Vector3.forward * t1 * data.BalloonLength;
            var l0 = r + data.StabilizerCurve.Evaluate(t) * l;
            var l1 = r + data.StabilizerCurve.Evaluate(t + step) * l;
            CreateStabilizerSegment(wireframe, c0, c1, dir, l0, l1, w / 2);
        }
        var c = Vector3.forward * x1 * data.BalloonLength;
        CreateStabilizerSegment(wireframe, c, c, dir, r + data.StabilizerCurve.Evaluate(1) * l, data.BalloonCurve.Evaluate(x1) * data.BalloonRadius, w / 2);
    }
    void CreateStabilizerSegment(Wireframe wireframe, Vector3 c0, Vector3 c1, Vector3 dir, float l0, float l1, float w)
    {
        var offset = Vector3.Cross(Vector3.forward, dir).normalized * w;
        var p0 = c0 + dir * l0;
        var p1 = c1 + dir * l1;
        wireframe.Connect(p0 + offset, p1 + offset);
        wireframe.Connect(p0 - offset, p1 - offset);
    }

    void CreateGondola(Wireframe wireframe, ShipMeshGeneratorData data)
    {
        var mtx = Matrix4x4.Translate(data.GondolaPosition);
        wireframe.PushMatrix(mtx);
        var step = Mathf.Max(0.05f, 1 / (float)data.GondolaSegments);
        for (float t = 0; t < 1; t += step)
        {
            var next = Mathf.Min(1, t + step);
            DrawGondolaSegment(wireframe, data, t, next);
        }
        wireframe.PopMatrix();
    }

    void DrawGondolaSegment(Wireframe wireframe, ShipMeshGeneratorData data, float t0, float t1)
    {
        var yStep = Mathf.Max(0.05f, 1 / (float)data.GondolaLayers);
        for (float y = 0; y < 1; y += yStep)
        {
            var k = data.GongolaKeelCurve.Evaluate(y);
            var k0 = Mathf.Lerp(k, 1, t0); 
            var k1 = Mathf.Lerp(k, 1, t1);
            var c0 = Vector3.forward * k0 * data.GondolaDimensions.z;
            var c1 = Vector3.forward * k1 * data.GondolaDimensions.z;
            var w0 = data.GondolaWidthCurve.Evaluate(t0) * data.GondolaVolumeCurve.Evaluate(y);
            var w1 = data.GondolaWidthCurve.Evaluate(t1) * data.GondolaVolumeCurve.Evaluate(y);
            var p0 = c0 + new Vector3(w0 * data.GondolaDimensions.x, -y * data.GondolaDimensions.y);
            var p1 = c1 + new Vector3(w1 * data.GondolaDimensions.x, -y * data.GondolaDimensions.y);
            wireframe.Connect(p0, p1);
            p0 = c0 + new Vector3(-w0 * data.GondolaDimensions.x, -y * data.GondolaDimensions.y);
            p1 = c1 + new Vector3(-w1 * data.GondolaDimensions.x, -y * data.GondolaDimensions.y);
            wireframe.Connect(p0, p1);
        }
    }
}