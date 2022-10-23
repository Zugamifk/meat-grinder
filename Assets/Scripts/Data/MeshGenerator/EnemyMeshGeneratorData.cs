using MeshGenerator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeshGeneratorData : ScriptableObject, IMeshGeneratorData
{
    public float Fatness;
    public float Height;
    public float HeadSize;
    public float HeadHeight;
    public Color BodyColor;
    public Color SkinColor;
}
