using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshGenerator
{
    public abstract class MeshGenerator : IGeometryGenerator
    {
        public abstract void Generate(MeshBuilder builder);
    }
}
