using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator;

public class MeshGeneratorUser : MonoBehaviour
{
    MeshBuilder _meshBuilder = new();
    protected void AssignMesh<TMeshGenerator>(MeshFilter meshFilter, System.Action<TMeshGenerator> initialize = null)
        where TMeshGenerator : MeshGenerator.MeshGenerator, new()
    {
        var gen = new TMeshGenerator();
        initialize?.Invoke(gen);
        gen.Generate(_meshBuilder);
        var mesh = _meshBuilder.BuildMesh();
        meshFilter.mesh = mesh;
    }
}
