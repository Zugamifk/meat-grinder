using MeshGenerator;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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

    public Vector3 GondolaPosition;
    public Vector3 GondolaDimensions;
    public AnimationCurve GondolaWidthCurve;
    public AnimationCurve GongolaKeelCurve;
    public AnimationCurve GondolaVolumeCurve;
    public float KeelBeamThickness;
    public float DeckBeamThickness;
    public int GondolaSegments;
    public int GondolaLayers;
}