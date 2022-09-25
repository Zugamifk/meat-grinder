using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator.Editor;
using MeshGenerator;
using UnityEditor;

[MeshGeneratorEditor(typeof(EndPortalMeshGenerator))]
public class EndPortalMeshGeneratorEditor : MeshGeneratorWithWireFrameEditor<EndPortalMeshGenerator, EndPortalMeshGeneratorData>
{

}
