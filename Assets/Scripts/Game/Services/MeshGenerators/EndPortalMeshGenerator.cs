using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator;
using MeshGenerator.Wireframe;

[MeshGenerator("End Portal")]
public class EndPortalMeshGenerator : MeshGeneratorWithData<EndPortalMeshGeneratorData>
{
    protected override void BuildMesh()
    {
        void AddColumn(float x, float y)
        {
            var b = new Vector3(x, 0, y);
            var p0 = b + new Vector3(-Data.ColumnSize, 0, -Data.ColumnSize);
            var p1 = b + new Vector3(-Data.ColumnSize, 0, Data.ColumnSize);
            var p2 = b + new Vector3(Data.ColumnSize, 0, Data.ColumnSize);
            var p3 = b + new Vector3(Data.ColumnSize, 0, -Data.ColumnSize);
            var p4 = p0 + new Vector3(0, Data.Height, 0);
            var p5 = p1 + new Vector3(0, Data.Height, 0);
            var p6 = p2 + new Vector3(0, Data.Height, 0);
            var p7 = p3 + new Vector3(0, Data.Height, 0);

            _builder.AddQuad(p0, p1, p5, p4);
            _builder.AddQuad(p1, p2, p6, p5);
            _builder.AddQuad(p2, p3, p7, p6);
            _builder.AddQuad(p3, p0, p4, p7);
        }

        _builder.SetColor(new Color(.75f, .75f, .75f, 1));
        AddColumn(-Data.ColumnSpacing, -Data.ColumnSpacing);
        AddColumn(Data.ColumnSpacing, -Data.ColumnSpacing);
        AddColumn(Data.ColumnSpacing, Data.ColumnSpacing);
        AddColumn(-Data.ColumnSpacing, Data.ColumnSpacing);

        void AddRoofStep(float w, float h)
        {
            var b = new Vector3(0, Data.Height + h, 0);
            var p0 = b + new Vector3(-w, 0, -w);
            var p1 = b + new Vector3(-w, 0, w);
            var p2 = b + new Vector3(w, 0, w);
            var p3 = b + new Vector3(w, 0, -w);
            var p4 = p0 + new Vector3(0, Data.RoofThickness, 0);
            var p5 = p1 + new Vector3(0, Data.RoofThickness, 0);
            var p6 = p2 + new Vector3(0, Data.RoofThickness, 0);
            var p7 = p3 + new Vector3(0, Data.RoofThickness, 0);

            _builder.AddQuad(p3, p2, p1, p0);
            _builder.AddQuad(p0, p1, p5, p4);
            _builder.AddQuad(p1, p2, p6, p5);
            _builder.AddQuad(p2, p3, p7, p6);
            _builder.AddQuad(p3, p0, p4, p7);
            _builder.AddQuad(p4, p5, p6, p7);
        }

        AddRoofStep(.5f, 0);
        AddRoofStep(.4f, Data.RoofThickness);
    }

    protected override EndPortalMeshGeneratorData LoadData() => DataService.GetData<MeshGeneratorDataCollection>().EndPortal;
}
