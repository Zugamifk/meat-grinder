using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator.Editor;
using MeshGenerator;
using MeshGenerator.Wireframes;

[MeshGeneratorEditor(typeof(ArrowTowerMeshGenerator))]
public class ArrowTowerMeshGeneratorEditor : MeshGeneratorEditorWithWireFrame<ArrowTowerMeshGenerator, ArrowTowerWireframeGenerator, ArrowTowerMeshGeneratorData>
{
}
