using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator.Editor;
using MeshGenerator;
using UnityEditor;
using MeshGenerator.Wireframes;

[MeshGeneratorEditor(typeof(EndPortalMeshGenerator))]
public class EndPortalMeshGeneratorEditor : MeshGeneratorEditorWithWireFrame<EndPortalMeshGenerator, EndPortalWireframeGenerator, EndPortalMeshGeneratorData>
{
}
