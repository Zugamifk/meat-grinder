using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator.Editor;
using MeshGenerator;
using UnityEditor;
using MeshGenerator.Wireframe;

[MeshGeneratorEditor(typeof(EnemyMeshGenerator))]
public class EnemyMeshGeneratorEditor : MeshGeneratorEditorWithWireFrame<EnemyMeshGenerator, EnemyMeshGeneratorData>
{
    public override void BuildWireframe()
    {
        Wireframe = new();

        Wireframe.SquareColumn(new Point(), () => _data.Height, () => _data.Fatness);
        Wireframe.SquareColumn(new DynamicPoint(() => new Vector3(0, _data.Height, 0)), () => _data.HeadHeight, () => _data.HeadSize);
    }
}
