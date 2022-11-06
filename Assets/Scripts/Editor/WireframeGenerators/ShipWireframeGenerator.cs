using MeshGenerator.Editor;
using MeshGenerator.Surfaces;
using MeshGenerator.Wireframes;
using PlasticGui.WebApi.Responses;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

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
        for (int i = 0; i < data.BalloonRingCount; i++)
        {
            for (int j = 0; j < data.BalloonRingSides; j++)
            {
                var u = (float)i / data.BalloonRingCount;
                var v = (float)j / data.BalloonRingSides;
                var p0 = data.GetBalloonPoint(u, 360 * v);
                u = (float)(i + 1) / data.BalloonRingCount;
                var p1 = data.GetBalloonPoint(u, 360 * v);
                wireframe.Connect(p0, p1);
            }
        }
    }

    void CreateStabilizer(Wireframe wireframe, ShipMeshGeneratorData data, Vector3 dir)
    {
        for (int i = 0; i < data.StabilizerSegments; i++)
        {
            var u = (float)i / data.StabilizerSegments;
            var p0 = data.GetStabilizerEdgePoint(u, dir);
            u = (float)(i + 1) / data.StabilizerSegments;
            var p1 = data.GetStabilizerEdgePoint(u, dir);
            wireframe.Connect(p0, p1);
        }
        wireframe.Connect(data.GetStabilizerEdgePoint(1, dir), data.GetBalloonPoint(data.StabilizerPosition.y, dir));
    }

    void CreateGondola(Wireframe wireframe, ShipMeshGeneratorData data)
    {
        var mtx = Matrix4x4.Translate(data.GondolaPosition);
        wireframe.PushMatrix(mtx);
        var zStep = Mathf.Max(0.05f, 1 / (float)data.GondolaSegments);
        var yStep = Mathf.Max(0.05f, 1 / (float)data.GondolaLayers);
        for (float t = 0; t < 1; t += zStep)
        {
            for (float y = 0; y < 1; y += yStep)
            {
                Vector3 l0, l1;
                var r0 = data.GetGondolaPoint(t, y, out l0);
                var r1 = data.GetGondolaPoint(t + zStep, y, out l1);
                wireframe.Connect(r0, r1);
                wireframe.Connect(l0, l1);
            }
        }
        wireframe.PopMatrix();
    }
}