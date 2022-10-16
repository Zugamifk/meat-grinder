using MeshGenerator.Wireframes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshGenerator.Editor
{
    public class EnemyWireframeGenerator : WireframeGenerator<EnemyMeshGeneratorData>
    {
        protected override void BuildWireframe(Wireframe wireframe, EnemyMeshGeneratorData data)
        {
            wireframe.SquareColumn(Vector3.zero, data.Height, data.Fatness);
            wireframe.SquareColumn(new Vector3(0, data.Height, 0), data.HeadHeight, data.HeadSize);
        }
    }
}
