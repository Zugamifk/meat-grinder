using MeshGenerator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Data/Mesh Generators/Ship")]
public class ShipMeshGeneratorData : ScriptableObject, IMeshGeneratorData
{
    public AnimationCurve BalloonCurve;
    public float BalloonRadius;
    public float BalloonLength;
    public float SegmentLength;
    public int NumRidges;

    public AnimationCurve StabilizerCurve;
    [MinMaxSlider]
    public Vector2 StabilizerPosition;
    public float StabilizerLength;
    public float StabilizerThickness;
    public int StabilizerSegments;
}