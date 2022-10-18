using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Data/Mesh Generators/GearAssembly")]
public class GearAssemblyMeshGeneratorData : ScriptableObject
{
    [System.Serializable]
    public class GearInfo
    {
        public GearMeshGeneratorData GeneratorData;
        public Vector3 Position;
        public float RotationOffset;
    }

    public GearInfo[] gears;
}