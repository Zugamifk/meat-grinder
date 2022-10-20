using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMap : MeshGeneratorUser
{
    [SerializeField]
    MeshFilter _meshFilter;

    private void Start()
    {
        AssignMesh<WorldMapMeshGenerator>(_meshFilter);
    }
}
