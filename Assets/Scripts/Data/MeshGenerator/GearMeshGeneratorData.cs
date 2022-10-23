using MeshGenerator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Data/Mesh Generators/Gear")]
public class GearMeshGeneratorData : ScriptableObject, IMeshGeneratorData
{
    public float Radius;
    public int TeethCount;
    public float WorkingDepth;
    public float ToothThickness;
    public float GearThickness;
    public float AxelRadius;
}