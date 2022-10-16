using MeshGenerator.Editor;
using MeshGenerator.Wireframes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapWireframeGenerator : WireframeGenerator<WorldMapMeshGeneratorData>
{
    protected override void BuildWireframe(Wireframe wireframe, WorldMapMeshGeneratorData data)
    {
        var w = data.PatchDimensions.x;
        var h = data.PatchDimensions.y;
        var ps = Vector2.Scale(data.PatchGridSize, new Vector2(1 / data.PatchDimensions.x, 1 / data.PatchDimensions.y));

        var c0 = Vector3.zero;
        var c1 = new Vector3(0, 0, h);
        var c2 = new Vector3(w, 0, h);
        var c3 = new Vector3(w, 0, 0);
        wireframe.Connect(c0, c1);
        wireframe.Connect(c1, c2);
        wireframe.Connect(c2, c3);
        wireframe.Connect(c3, c0);

        var gx = data.PatchGridSize.x;
        var gy = data.PatchGridSize.y;
        var xStep = w / gx;
        var yStep = h / gy;

        Vector3 GetOffset(int x, int y)
        {
            return Random.insideUnitCircle;
        }

        for (int x = 0; x < gx; x++)
        {
            for (int y = 0; y < gy; y++)
            {
                if (y < gy - 1)
                {
                    var x0 = x;
                    var y0 = y;
                    var p0 = new Vector3(x0 * xStep, 0, (y0 + 1) * yStep);
                    if (x0 > 0)
                    {
                        p0 += GetOffset(x0 - 1, y0) * data.PointVariance;
                    }

                    var p1 = new Vector3((x0 + 1) * xStep, 0, (y0 + 1) * yStep);
                    if (x0 < gx - 1)
                    {
                        p1 += GetOffset(x0, y0) * data.PointVariance;
                    }
                    wireframe.Connect(p0, p1);
                }

                if (x < gx - 1)
                {
                    var x0 = x;
                    var y0 = y;
                    var p0 = new Vector3((x0 + 1) * xStep, 0, y0 * yStep);
                    if (y0 > 0)
                    {
                        p0 += GetOffset(x0, y0 - 1) * data.PointVariance;
                    }
                    var p1 = new Vector3((x0 + 1) * xStep, 0, (y0 + 1) * yStep);
                    if (y0 < gy - 1)
                    {
                        p1 += GetOffset(x0, y0) * data.PointVariance;
                    }
                    wireframe.Connect(p0, p1);
                }
            }
        }
    }
}
