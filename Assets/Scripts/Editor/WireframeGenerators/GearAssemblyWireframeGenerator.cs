using MeshGenerator.Editor;
using MeshGenerator.Wireframes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearAssemblyWireframeGenerator : WireframeGenerator<GearAssemblyMeshGeneratorData>
{
    GearWireframeGenerator _gearGenerator = new();

    protected override void BuildWireframe(Wireframe wireframe, GearAssemblyMeshGeneratorData data)
    {
        foreach(var gear in data.gears)
        {
            if(gear.GeneratorData!=null)
            {
                DrawGear(wireframe, gear);
            }
        }
    }

    void DrawGear(Wireframe wireframe, GearAssemblyMeshGeneratorData.GearInfo gear)
    {
        wireframe.PushMatrix(Matrix4x4.TRS(gear.Position, Quaternion.AngleAxis(gear.RotationOffset, Vector3.forward), Vector3.one));
        _gearGenerator.Generate(wireframe, gear.GeneratorData);
        wireframe.PopMatrix();
    }
}
