using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator;
using System;
using MeshGenerator.Wireframe;

[MeshGenerator("WorldMap")]
public class WorldMapMeshGenerator : MeshGeneratorWithData<WorldMapMeshGeneratorData>
{
    //Vector2[,] _tileOffsets;
    //Vector2[,] _cornerOffsets;

    //public Vector2 GetOffset(int x, int y) => _cornerOffsets[x, y];

    //public void GenerateOffsets()
    //{
    //    _tileOffsets = new Vector2[Data.PatchGridSize.x, Data.PatchGridSize.y];
    //    _cornerOffsets = new Vector2[Data.PatchGridSize.x-1, Data.PatchGridSize.y-1];
    //    for (int x=0;x<Data.PatchGridSize.x;x++)
    //    {
    //        for(int y = 0; y < Data.PatchGridSize.y; y++)
    //        {
    //            _tileOffsets[x, y] = UnityEngine.Random.insideUnitCircle.normalized;
    //            if(x>0&&y>0)
    //            {
    //                var sum = _tileOffsets[x - 1, y - 1] + _tileOffsets[x, y - 1] + _tileOffsets[x - 1, y] + _tileOffsets[x, y];
    //                _cornerOffsets[x - 1, y - 1] = sum / 4;
    //            }
    //        }
    //    }
    //}

    protected override MeshGeneratorResult BuildMesh()
    {
        return new();
    }

    protected override WorldMapMeshGeneratorData LoadData() => DataService.GetData<MeshGeneratorDataCollection>().WorldMap;
}
