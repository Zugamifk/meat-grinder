using MeshGenerator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTowerMeshGeneratorData : ScriptableObject, IMeshGeneratorData
{
    public float Height;
    public Vector2 RadiusMinMax;
    public AnimationCurve TowerCurve;
    public float Angle;
    public float LayerStep;
}

