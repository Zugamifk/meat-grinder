using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshGenerator
{
    public abstract class MeshGenerator : IGeometryGenerator
    {
        protected MeshBuilder _builder = new();
        public abstract MeshGeneratorResult Generate();
    }
}
