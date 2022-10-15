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
            BuildMesh();
            return BuildResult();
        }

        protected virtual MeshGeneratorResult BuildResult()
        {
            var result = new MeshGeneratorResult();
            result.Mesh = _builder.BuildMesh();
            return result;
        }

        protected abstract void BuildMesh();
        public void Clear()
        {
            _builder.Clear();
        }
    }
}
