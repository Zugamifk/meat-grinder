using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator.Editor;
using MeshGenerator;
using UnityEditor;
using MeshGenerator.Wireframe;

[MeshGeneratorEditor(typeof(EndPortalMeshGenerator))]
public class EndPortalMeshGeneratorEditor : MeshGeneratorEditorWithWireFrame<EndPortalMeshGenerator, EndPortalMeshGeneratorData>
{
    public override void BuildWireframe()
    {
        Wireframe = new();

        var b = .5f;
        var b0 = new Point(-b, 0, -b);
        var b1 = new Point(-b, 0, b);
        var b2 = new Point(b, 0, b);
        var b3 = new Point(b, 0, -b);

        // base
        Wireframe.Connect(b0, b1);
        Wireframe.Connect(b1, b2);
        Wireframe.Connect(b2, b3);
        Wireframe.Connect(b3, b0);

        // columns
        Wireframe.SquareColumn(new DynamicPoint(() => new Vector3(-_data.ColumnSpacing, 0, -_data.ColumnSpacing)), () => _data.Height, () => _data.ColumnSize);
        Wireframe.SquareColumn(new DynamicPoint(() => new Vector3(-_data.ColumnSpacing, 0, _data.ColumnSpacing)), () => _data.Height, () => _data.ColumnSize);
        Wireframe.SquareColumn(new DynamicPoint(() => new Vector3(_data.ColumnSpacing, 0, _data.ColumnSpacing)), () => _data.Height, () => _data.ColumnSize);
        Wireframe.SquareColumn(new DynamicPoint(() => new Vector3(_data.ColumnSpacing, 0, -_data.ColumnSpacing)), () => _data.Height, () => _data.ColumnSize);

        // roof
        Wireframe.SquareColumn(new DynamicPoint(() => new Vector3(0, _data.Height, 0)), () => _data.RoofThickness, () => .5f);
        Wireframe.SquareColumn(new DynamicPoint(() => new Vector3(0, _data.Height + _data.RoofThickness, 0)), () => _data.RoofThickness, () => .4f);
    }

}
