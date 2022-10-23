using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator;

[CreateAssetMenu(menuName="Data/Mesh Generators/GearAssembly")]
public class GearAssemblyMeshGeneratorData : ScriptableObject, IMeshGeneratorData
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