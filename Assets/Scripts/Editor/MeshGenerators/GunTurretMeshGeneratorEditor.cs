using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator.Editor;
using MeshGenerator;
using MeshGenerator.Wireframes;
using System;
using UnityEditor;

[MeshGeneratorEditor(typeof(GunTurretMeshGenerator))]
public class GunTurretMeshGeneratorEditor : MeshGeneratorEditorWithWireFrame<GunTurretMeshGenerator, GunTurretWireframeGenerator, GunTurretMeshGeneratorData>
{
}
