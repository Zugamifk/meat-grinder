using MeshGenerator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTurretMeshGeneratorData : ScriptableObject, IMeshGeneratorData
{
    public Vector2 BaseDimensions;
    public Vector3 GunBounds;
    public Vector3 MountPosition;
    public Vector2 BarrelDimensions;
    public float MountedAngle;
    public ScriptedAnimationData ShootBarrelRecoilCurve;
}
