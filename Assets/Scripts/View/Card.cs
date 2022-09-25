using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MeshGenerator;

public class Card : MeshGeneratorUser
{
    [SerializeField] MeshFilter _meshFilter;

    private void Start()
    {
        AssignMesh<CardFrameMeshGenerator>(_meshFilter);
    }
}
