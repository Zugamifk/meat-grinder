using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator.Editor;
using MeshGenerator;
using MeshGenerator.Wireframe;
using UnityEngine.TestTools.Utils;
using System;

[MeshGeneratorEditor(typeof(GearMeshGenerator))]
public class GearMeshGeneratorEditor : MeshGeneratorEditorWithWireFrame<GearMeshGenerator, GearMeshGeneratorData>
{
    int _currentTeeth;

    protected override void OnPropertiesChanged()
    {
        if (_data.TeethCount != _currentTeeth)
        {
            BuildWireframe();
        }
    }

    public override void BuildWireframe()
    {
        _currentTeeth = _data.TeethCount;

        _wireframe = new();
        Func<float> ang = () => 360f / (float)_data.TeethCount;
        Func<Vector3> d = () => Vector3.right;
        Func<Quaternion> toothRot = () => Quaternion.Euler(0, 0, ang() * _data.ToothThickness);
        Func<Quaternion> climbRot = () => Quaternion.Euler(0, 0, ang() * (0.5f - _data.ToothThickness));
        Func<float> r0 = () => _data.Radius;
        Func<float> r1 = () => r0() + _data.WorkingDepth;
        var points = new List<IPoint>();
        for (int i = 0; i < _data.TeethCount; i++)
        {
            var d0 = d;
            Func<Vector3> d1 = () => toothRot() * d0();
            Func<Vector3> d2 = () => climbRot() * d1();
            Func<Vector3> d3 = () => toothRot() * d2();
            Func<Vector3> d4 = () => climbRot() * d3();
            points.Add(new DynamicPoint(() => d0() * r0()));
            points.Add(new DynamicPoint(() => d1() * r0()));
            points.Add(new DynamicPoint(() => d2() * r1()));
            points.Add(new DynamicPoint(() => d3() * r1()));
            d = d4;
        }
        _wireframe.Prism(points, () => _data.GearThickness);
    }
}
