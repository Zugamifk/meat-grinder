using Codice.Client.BaseCommands.Merge;
using MeshGenerator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoGrid : MeshGeneratorUser
{
    [SerializeField] MeshFilter _meshFilter;
    [SerializeField] Vector2 _dimensions;
    [SerializeField] Vector2Int _grid;

    private void Start()
    {
        AssignMesh<DemoGridMeshGenerator>(_meshFilter,
            gen =>
            {
                gen.Dimensions = _dimensions;
                gen.Grid = _grid;
            });
    }
}
