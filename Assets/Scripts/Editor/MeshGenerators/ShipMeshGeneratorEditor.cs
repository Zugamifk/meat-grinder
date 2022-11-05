using MeshGenerator.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[MeshGeneratorEditor(typeof(ShipMeshGenerator))]
public class ShipMeshGeneratorEditor : MeshGeneratorEditorWithWireFrame<ShipMeshGenerator, ShipWireframeGenerator, ShipMeshGeneratorData>
{
}

