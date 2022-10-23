using MeshGenerator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Data/Mesh Generators/WorldMap")]
public class WorldMapMeshGeneratorData : ScriptableObject, IMeshGeneratorData
{
    public Vector2 PatchDimensions;
    public Vector2Int PatchGridSize;
    [Range(0,1)]
    public float PointVariance;
}