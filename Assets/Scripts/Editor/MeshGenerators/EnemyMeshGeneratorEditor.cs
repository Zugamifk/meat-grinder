using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator.Editor;
using MeshGenerator;
using UnityEditor;
using MeshGenerator.Wireframes;

[MeshGeneratorEditor(typeof(EnemyMeshGenerator))]
public class EnemyMeshGeneratorEditor : MeshGeneratorEditorWithWireFrame<EnemyMeshGenerator, EnemyWireframeGenerator, EnemyMeshGeneratorData>
{
    
}
