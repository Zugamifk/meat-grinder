using MeshGenerator.Editor;
using MeshGenerator.Surfaces;
using MeshGenerator.Wireframes;
using PlasticGui.WebApi.Responses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipWireframeGenerator : WireframeGenerator<ShipMeshGeneratorData>
{

    protected override void BuildWireframe(Wireframe wireframe, ShipMeshGeneratorData data)
    {
        CreateBalloon(wireframe, data);
    }

    void CreateBalloon(Wireframe wireframe, ShipMeshGeneratorData data)
    {
        var step = Mathf.Max(0.05f, data.SegmentLength)/data.BalloonLength;
        var sides = data.NumRidges;
        var dir = Vector3.up;
        for(float t = 0; t < 1; t += step)
        {
            var next = Mathf.Min(t + step, 1);
            var c0 = Vector3.forward * t * data.BalloonLength;
            var c1 = Vector3.forward * next * data.BalloonLength;
            var r0 = data.BalloonCurve.Evaluate(t) * data.BalloonRadius;
            var r1 = data.BalloonCurve.Evaluate(next) * data.BalloonRadius;
            CreateSegment(wireframe, dir, c0, c1, r0, r1, data.NumRidges);
        }
        var start = Vector3.zero;
        CreateSegment(wireframe, dir, start, start, data.BalloonCurve.Evaluate(0) * data.BalloonRadius, 0, data.NumRidges);
        var end = Vector3.forward * data.BalloonLength;
        CreateSegment(wireframe, dir, end, end, data.BalloonCurve.Evaluate(1) * data.BalloonRadius, 0, data.NumRidges);
    }

    void CreateSegment(Wireframe wireframe, Vector3 dir, Vector3 c0, Vector3 c1, float r0, float r1, int sides)
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

    void CreateDeck()
    {

    }
}