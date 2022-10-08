using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MeshGenerator
{
    [MeshGenerator("Surface Model")]
    public class SurfaceModelMeshGenerator : MeshGenerator
    {
        SurfaceModel _model;

        public SurfaceModel Model => Model;

        public SurfaceModelMeshGenerator()
        {
            //var cb = new CubeGenerator();
            //var b = new MeshBuilder();
            //cb.Generate(b);
            //var m = b.Build();
            //var smb = new MeshToSurfaceModelBuilder();
            //_model = smb.ConvertMesh(m);
        }

        protected override MeshGeneratorResult BuildMesh()
        {
            foreach(var f in _model.Faces)
            {
                _builder.AddPolygon(f.HalfEdge.Loop().Select(he => he.Vertex.Position).ToArray());
            }

            var result = new MeshGeneratorResult();
            result.Mesh = _builder.BuildMesh();
            return result;
        }
    }
}
