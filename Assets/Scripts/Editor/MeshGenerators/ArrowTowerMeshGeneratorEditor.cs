using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator.Editor;
using MeshGenerator;
using MeshGenerator.Wireframes;

[MeshGeneratorEditor(typeof(ArrowTowerMeshGenerator))]
public class ArrowTowerMeshGeneratorEditor : MeshGeneratorEditorWithWireFrame<ArrowTowerMeshGenerator, ArrowTowerMeshGeneratorData>
{
    protected override void BuildWireframe()
    {
        void AddRing(float t)
        {
            _wireframe.Rings.Add(new Ring()
            {
                Center = new DynamicPoint(() => new Vector3(0, t * _data.Height, 0)),
                Normal = () => Vector3.up,
                Radius = () => Mathf.Lerp(_data.RadiusMinMax.x, _data.RadiusMinMax.y, _data.TowerCurve.Evaluate(t))
            });
        }

        for (float t = 0; t < 1; t += 0.25f / _data.Height)
        {
            AddRing(t);
        }
        AddRing(1);
    }
}
