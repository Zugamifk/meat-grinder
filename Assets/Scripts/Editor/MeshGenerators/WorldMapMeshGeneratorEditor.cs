using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator.Editor;
using MeshGenerator;
using MeshGenerator.Wireframe;
using System;
using System.Runtime.InteropServices;

[MeshGeneratorEditor(typeof(WorldMapMeshGenerator))]
public class WorldMapMeshGeneratorEditor : MeshGeneratorEditorWithWireFrame<WorldMapMeshGenerator, WorldMapMeshGeneratorData>
{
    protected override void OnPropertiesChanged()
    {
        _generator.GenerateOffsets();
    }

    protected override void OnSetGenerator()
    {
        _generator.GenerateOffsets();
    }

    protected override void BuildWireframe()
    {
        Func<float> w = () => _data.PatchDimensions.x;
        Func<float> h = () => _data.PatchDimensions.y;
        Func<Vector2> ps = () => Vector2.Scale(_data.PatchGridSize, new Vector2(1/_data.PatchDimensions.x, 1/_data.PatchDimensions.y));
        _wireframe = new();
        var c0 = new Point();
        var c1 = new DynamicPoint(() => new Vector3(0, 0, h()));
        var c2 = new DynamicPoint(() => new Vector3(w(), 0, h()));
        var c3 = new DynamicPoint(() => new Vector3(w(), 0, 0));
        _wireframe.Connect(c0, c1);
        _wireframe.Connect(c1, c2);
        _wireframe.Connect(c2, c3);
        _wireframe.Connect(c3, c0);

        Func<int> gx = () => _data.PatchGridSize.x;
        Func<int> gy = () => _data.PatchGridSize.y;
        Func<float> xStep = () => w() / gx();
        Func<float> yStep = () => h() / gy();

        Vector3 GetOffset(int x, int y)
        {
            var offset = _generator.GetOffset(x, y);
            return new Vector3(offset.x, 0, offset.y);
        }

        for (int x = 0; x < gx(); x++)
        {
            for (int y = 0; y < gy(); y++)
            {
                if(y < gy() - 1)
                {
                    var x0 = x;
                    var y0 = y;
                    var p0 = new DynamicPoint(() =>
                    {
                        var p = new Vector3(x0 * xStep(), 0, (y0 + 1) * yStep());
                        if(x0 > 0)
                        {
                            p += GetOffset(x0-1, y0) * _data.PointVariance;
                        }
                        return p;
                    });
                    var p1 = new DynamicPoint(() =>
                    {
                        var p = new Vector3((x0+1) * xStep(), 0, (y0 + 1) * yStep());
                        if (x0 < gx() - 1)
                        {
                            p += GetOffset(x0, y0) * _data.PointVariance;
                        }
                        return p;
                    });
                    _wireframe.Connect(p0, p1);
                }

                if (x < gx() - 1)
                {
                    var x0 = x;
                    var y0 = y;
                    var p0 = new DynamicPoint(() =>
                    {
                        var p = new Vector3((x0 +1) * xStep(), 0, y0 * yStep());
                        if (y0 > 0)
                        {
                            p += GetOffset(x0, y0-1) * _data.PointVariance;
                        }
                        return p;
                    });
                    var p1 = new DynamicPoint(() =>
                    {
                        var p = new Vector3((x0 + 1) * xStep(), 0, (y0 + 1) * yStep());
                        if (y0 < gy() - 1)
                        {
                            p += GetOffset(x0, y0) * _data.PointVariance;
                        }
                        return p;
                    });
                    _wireframe.Connect(p0, p1);
                }
            }
        }
    }
}
