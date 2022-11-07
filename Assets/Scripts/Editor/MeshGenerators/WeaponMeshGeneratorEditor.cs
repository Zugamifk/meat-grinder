using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator.Editor;
using MeshGenerator;

[MeshGeneratorEditor(typeof(WeaponMeshGenerator))]
public class WeaponMeshGeneratorEditor : MeshGeneratorEditorWithWireFrame<WeaponMeshGenerator, WeaponWireframeGenerator, WeaponMeshGeneratorData>
{

}
