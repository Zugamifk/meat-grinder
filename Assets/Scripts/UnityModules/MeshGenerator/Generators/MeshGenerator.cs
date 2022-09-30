using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshGenerator
{
    public abstract class MeshGenerator : IGeometryGenerator
    {
        protected MeshBuilder _builder = new();

        public MeshGeneratorResult Generate()
        {
            _builder.Clear();
            return BuildMesh();
        }

        protected abstract MeshGeneratorResult BuildMesh();
        public void Clear()
        {
            _builder.Clear();
        }
    }
}
