using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshGenerator
{
    public class MeshData
    {
        public List<Vector3> Vertices = new();
        public List<Vector3> Normals = new();
        public List<Color> Colors = new();
        public List<int> Triangles = new();
    }
}
