using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator.Editor;
using MeshGenerator;
using MeshGenerator.Wireframes;
using UnityEngine.TestTools.Utils;
using System;
using UnityEditor;

[MeshGeneratorEditor(typeof(GearMeshGenerator))]
public class GearMeshGeneratorEditor : MeshGeneratorEditorWithWireFrame<GearMeshGenerator, GearWireframeGenerator, GearMeshGeneratorData>
{
}
