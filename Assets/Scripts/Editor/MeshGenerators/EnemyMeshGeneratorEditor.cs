using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator.Editor;
using MeshGenerator;
using UnityEditor;

[MeshGeneratorEditor(typeof(EnemyMeshGenerator))]
public class EnemyMeshGeneratorEditor : MeshGeneratorWithWireFrameEditor<EnemyMeshGenerator, EnemyMeshGeneratorData>
{

}
