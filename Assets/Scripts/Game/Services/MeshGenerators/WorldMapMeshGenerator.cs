using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator;
using System;
using MeshGenerator.Wireframes;
using System.Security.Cryptography;
using UnityEditor.Android;

[MeshGenerator("WorldMap")]
public class WorldMapMeshGenerator : MeshGeneratorWithData<WorldMapMeshGeneratorData>
{
    Vector2[,] _tileOffsets;
    Vector2[,] _cornerOffsets;

    public Vector2 GetOffset(int x, int y) => _cornerOffsets[x, y];

    public void GenerateOffsets()
    {
        _tileOffsets = new Vector2[Data.PatchGridSize.x, Data.PatchGridSize.y];
        _cornerOffsets = new Vector2[Data.PatchGridSize.x - 1, Data.PatchGridSize.y - 1];
        var rnd = new System.Random();
        for (int x = 0; x < Data.PatchGridSize.x; x++)
        {
            for (int y = 0; y < Data.PatchGridSize.y; y++)
            {
                _tileOffsets[x, y] = UnityEngine.Random.insideUnitCircle;
                if (x > 0 && y > 0)
                {
                    var sum = _tileOffsets[x - 1, y - 1] + _tileOffsets[x, y - 1] + _tileOffsets[x - 1, y] + _tileOffsets[x, y];
                    _cornerOffsets[x - 1, y - 1] = sum / 4;
                }
            }
        }
    }

    protected override void BuildMesh(MeshBuilder builder)
    {
        builder.SetColor(Colorx.FromHex(0x90AD6B));

        var w = Data.PatchDimensions.x;
        var h = Data.PatchDimensions.y;
        var ps = Vector2.Scale(Data.PatchGridSize, new Vector2(1 / Data.PatchDimensions.x, 1 / Data.PatchDimensions.y));

        var gx = Data.PatchGridSize.x;
        var gy = Data.PatchGridSize.y;
        var xs = w / gx;
        var ys = h / gy;
        var x0 = -w / 2;
        var y0 = -h / 2;

        for (int x = 0; x < gx; x++)
        {
            for (int y = 0; y < gy; y++)
            {
                builder.AddQuad(new Vector3(x0 + x * xs, 0, y0 + y * ys),
                    new Vector3(x0+x * xs, 0, y0+(y + 1) * ys),
                    new Vector3(x0+(x + 1) * xs, 0, y0 + (y + 1) * ys),
                    new Vector3(x0+(x + 1) * xs, 0, y0 + y * ys));
            }
        }
    }

    protected override WorldMapMeshGeneratorData LoadData() => DataService.GetData<MeshGeneratorDataCollection>().WorldMap;
}
