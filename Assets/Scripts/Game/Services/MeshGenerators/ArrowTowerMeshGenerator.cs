using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator;
using MeshGenerator.Wireframe;

[MeshGenerator("Arrow Tower")]
public class ArrowTowerMeshGenerator : MeshGeneratorWithWireFrame<ArrowTowerMeshGeneratorData>
{
    public override void BuildWireframe()
    {
        Wireframe = new();
        void AddRing(float t)
        {
            var r = Mathf.Lerp(Data.RadiusMinMax.x, Data.RadiusMinMax.y, Data.TowerCurve.Evaluate(t));
            Wireframe.Rings.Add(new Ring()
            {
                Center = new DynamicPoint(() => new Vector3(0, t * Data.Height, 0)),
                Normal = Vector3.up,
                Radius = r
            });
        }

        for (float t = 0; t < 1; t += 0.25f / Data.Height)
        {
            AddRing(t);
        }
        AddRing(1);
    }

    public override MeshGeneratorResult Generate()
    {
        _builder.SetColor(Color.white);
        var top = new List<Vector3>();
        for (float a0 = 0; a0 < 360; a0 += Data.Angle)
        {
            float a1 = Mathf.Min(360, a0 + Data.Angle);
            var d0 = Quaternion.AngleAxis(a0, Vector3.up) * Vector3.right;
            var d1 = Quaternion.AngleAxis(a1, Vector3.up) * Vector3.right;
            var layerStep = Data.LayerStep / Data.Height;
            for (float t = 0; t < 1; t += layerStep)
            {
                var t0 = Data.TowerCurve.Evaluate(t);
                var t1 = Data.TowerCurve.Evaluate(t + layerStep);
                var r0 = Mathf.Lerp(Data.RadiusMinMax.x, Data.RadiusMinMax.y, t0);
                var r1 = Mathf.Lerp(Data.RadiusMinMax.x, Data.RadiusMinMax.y, t1);
                var p0 = new Vector3(0, t * Data.Height, 0) + d0 * r0;
                var p1 = new Vector3(0, t * Data.Height, 0) + d1 * r0;
                var p2 = new Vector3(0, (t + layerStep) * Data.Height, 0) + d1 * r1;
                var p3 = new Vector3(0, (t + layerStep) * Data.Height, 0) + d0 * r1;
                _builder.AddQuad(p0, p1, p2, p3);
            }
            var tt = Data.TowerCurve.Evaluate(1);
            var r = Mathf.Lerp(Data.RadiusMinMax.x, Data.RadiusMinMax.y, tt);
            var p = new Vector3(0, Data.Height, 0) + d0 * r;
            top.Add(p);
        }
        _builder.AddPolygon(top);

        var result = new MeshGeneratorResult();
        result.Meshes.Add(_builder.BuildMesh());
        return result;
    }

    protected override ArrowTowerMeshGeneratorData LoadData() => DataService.GetData<MeshGeneratorDataCollection>().ArrowTower;
}
