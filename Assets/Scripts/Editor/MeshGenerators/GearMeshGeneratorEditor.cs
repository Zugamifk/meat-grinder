using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator.Editor;
using MeshGenerator;
using MeshGenerator.Wireframe;
using UnityEngine.TestTools.Utils;
using System;
using UnityEditor;

[MeshGeneratorEditor(typeof(GearMeshGenerator))]
public class GearMeshGeneratorEditor : MeshGeneratorEditorWithWireFrame<GearMeshGenerator, GearMeshGeneratorData>
{
    protected override void BuildWireframe()
    {
        Debug.Log("Build wireframe");

        var ang = 360f / (float)_data.TeethCount;
        var d = Vector3.right;
        var toothRot = Quaternion.Euler(0, 0, ang * _data.ToothThickness);
        var climbRot = Quaternion.Euler(0, 0, ang * (0.5f - _data.ToothThickness));
        var r0 = _data.Radius;
        var r1 = r0 + _data.WorkingDepth;
        var points = new List<IPoint>();
        for (int i = 0; i < _data.TeethCount; i++)
        {
            var d0 = d;
            var d1 = climbRot * d0;
            var d2 = toothRot * d1;
            var d3 = climbRot * d2;
            var d4 = toothRot * d3;
            points.Add(new Point(d0 * r0));
            points.Add(new Point(d1 * r1));
            points.Add(new Point(d2 * r1));
            points.Add(new Point(d3 * r0));
            d = d4;
        }
        _wireframe.Prism(points, () => Vector3.forward, () => _data.GearThickness);
        _wireframe.Cylinder(() => Vector3.zero, () => _data.AxelRadius, () => _data.GearThickness, () => Vector3.forward, () => SceneView.currentDrawingSceneView.camera.transform.forward);

        //Func<float> ang = () => 360f / (float)_data.TeethCount;
        //Func<Vector3> d = () => Vector3.right;
        //Func<Quaternion> toothRot = () => Quaternion.Euler(0, 0, ang() * _data.ToothThickness);
        //Func<Quaternion> climbRot = () => Quaternion.Euler(0, 0, ang() * (0.5f - _data.ToothThickness));
        //Func<float> r0 = () => _data.Radius;
        //Func<float> r1 = () => r0() + _data.WorkingDepth;
        //var points = new List<IPoint>();
        //for (int i = 0; i < _data.TeethCount; i++)
        //{
        //    var d0 = d;
        //    Func<Vector3> d1 = () => climbRot() * d0();
        //    Func<Vector3> d2 = () => toothRot() * d1();
        //    Func<Vector3> d3 = () => climbRot() * d2();
        //    Func<Vector3> d4 = () => toothRot() * d3();
        //    points.Add(new DynamicPoint(() => d0() * r0()));
        //    points.Add(new DynamicPoint(() => d1() * r1()));
        //    points.Add(new DynamicPoint(() => d2() * r1()));
        //    points.Add(new DynamicPoint(() => d3() * r0()));
        //    d = d4;
        //}
        //_wireframe.Prism(points, () => Vector3.forward, () => _data.GearThickness);
        //_wireframe.Cylinder(()=>Vector3.zero, ()=>_data.AxelRadius, ()=>_data.GearThickness, ()=>Vector3.forward,()=>SceneView.currentDrawingSceneView.camera.transform.forward);
    }
}
