using System.Collections;
using System.Collections.Generic;
using MeshGenerator.Editor;
using MeshGenerator;
using UnityEditor;

[MeshGeneratorEditor(typeof(CardFrameMeshGenerator))]
public class CardFrameMeshGeneratorEditor : MeshGeneratorWithWireFrameEditor<CardFrameMeshGenerator, CardFrameMeshGeneratorData>
{

}
