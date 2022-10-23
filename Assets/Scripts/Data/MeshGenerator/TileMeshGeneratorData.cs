using MeshGenerator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMeshGeneratorData : ScriptableObject, IMeshGeneratorData
{
    public Color GrassColor;
    public Color PathColor;
    public Color WallColor;
    public float TileStepHeight;
    public float PathWidth;
    public float WallInset;
}
