using MeshGenerator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Data/Mesh Generators/Ship")]
public class ShipMeshGeneratorData : ScriptableObject, IMeshGeneratorData
{
    public AnimationCurve BalloonCurve;
    public float BalloonRadius;
    public float BaloonLength;
}