using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MeshGenerator
{
    public class MeshGeneratorResult
    {
        public Dictionary<string, Mesh> Meshes = new();
        public Dictionary<string, Vector3> SpecialBones = new();
        public Mesh MainMesh => Meshes.Values.First();
    }
}
