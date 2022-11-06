using MeshGenerator;
using MeshGenerator.Wireframes;
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
    public int BalloonRingCount;
    public int BalloonRingSides;

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

    /// <summary>
    /// Get a point on the surface of the balloon
    /// </summary>
    /// <param name="lengthParam"></param>
    /// <param name="angleParam"></param>
    /// <returns></returns>
    public Vector3 GetBalloonPoint(float lengthParam, float angleParam)
    {
        var c = Vector3.forward * lengthParam * BalloonLength;
        var dir = Quaternion.AngleAxis(angleParam, Vector3.forward) * Vector3.up;
        var radius = BalloonRadius * BalloonCurve.Evaluate(lengthParam);
        return c + dir * radius;
    }

    public Vector3 GetBalloonPoint(float lengthParam, Vector3 dir)
    {
        var c = Vector3.forward * lengthParam * BalloonLength;
        var radius = BalloonRadius * BalloonCurve.Evaluate(lengthParam);
        return c + dir * radius;
    }

    /// <summary>
    /// Get a point along the edge of a stabilizer
    /// </summary>
    /// <param name="lengthParam"></param>
    /// <param name="direction"></param>
    /// <returns></returns>
    public Vector3 GetStabilizerEdgePoint(float lengthParam, Vector3 direction)
    {
        var t = Mathf.Lerp(StabilizerPosition.x, StabilizerPosition.y, lengthParam);
        var r = BalloonRadius * BalloonCurve.Evaluate(StabilizerPosition.x);
        var s = StabilizerLength * StabilizerCurve.Evaluate(lengthParam);
        var c = Vector3.forward * BalloonLength * t;
        return c + direction * (r + s);
    }

    public Vector3 GetGondolaPoint(float lengthParam, float heightParam, out Vector3 opposite)
    {
        var k = GongolaKeelCurve.Evaluate(heightParam);
        var k0 = Mathf.Lerp(k, 1, lengthParam);
        var c = Vector3.forward * k0 * GondolaDimensions.z;
        
        var w = GondolaWidthCurve.Evaluate(lengthParam) * GondolaVolumeCurve.Evaluate(heightParam);
        opposite = c + new Vector3(w * GondolaDimensions.x, -heightParam * GondolaDimensions.y);
        return c + new Vector3(-w * GondolaDimensions.x, -heightParam * GondolaDimensions.y);
    }
}