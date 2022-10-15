using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Data/Mesh Generators/Gear")]
public class GearMeshGeneratorData : ScriptableObject
{
    public float Radius;
    public int TeethCount;
    public float WorkingDepth;
    public float ToothThickness;
    public float GearThickness;
}