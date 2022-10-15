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
        if(_data.TeethCount != _currentTeeth)
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
        for (int i = 0; i < _data.TeethCount; i++)
        {
            var d0 = d;
            var p0 = new DynamicPoint(() => d0() * r0());
            Func<Vector3> d1 = () => toothRot() * d0();
            var p1 = new DynamicPoint(() => d1() * r0());
            Func<Vector3> d2 = () => climbRot() * d1();
            var p2 = new DynamicPoint(() => d2() * r1());
            Func<Vector3> d3 = () => toothRot() * d2();
            var p3 = new DynamicPoint(() => d3() * r1());
            Func<Vector3> d4 = () => climbRot() * d3();
            var p4 = new DynamicPoint(() => d4() * r0());
            _wireframe.Connect(p0, p1);
            _wireframe.Connect(p1, p2);
            _wireframe.Connect(p2, p3);
            _wireframe.Connect(p3, p4);
            d = d4;
        }
    }
}
