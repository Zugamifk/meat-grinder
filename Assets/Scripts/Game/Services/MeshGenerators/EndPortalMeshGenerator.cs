using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator;
using MeshGenerator.Wireframe;

[MeshGenerator("End Portal")]
public class EndPortalMeshGenerator : MeshGeneratorWithWireFrame<EndPortalMeshGeneratorData>
{
    public override void Generate(MeshBuilder builder)
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

            builder.AddQuad(p0, p1, p5, p4);
            builder.AddQuad(p1, p2, p6, p5);
            builder.AddQuad(p2, p3, p7, p6);
            builder.AddQuad(p3, p0, p4, p7);
        }

        builder.SetColor(new Color(.75f, .75f, .75f, 1));
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

            builder.AddQuad(p3, p2, p1, p0);
            builder.AddQuad(p0, p1, p5, p4);
            builder.AddQuad(p1, p2, p6, p5);
            builder.AddQuad(p2, p3, p7, p6);
            builder.AddQuad(p3, p0, p4, p7);
            builder.AddQuad(p4, p5, p6, p7);
        }

        AddRoofStep(.5f, 0);
        AddRoofStep(.4f, Data.RoofThickness);
    }

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
        Wireframe.SquareColumn(new DynamicPoint(() => new Vector3(-Data.ColumnSpacing, 0, -Data.ColumnSpacing)), () => Data.Height, () => Data.ColumnSize);
        Wireframe.SquareColumn(new DynamicPoint(() => new Vector3(-Data.ColumnSpacing, 0, Data.ColumnSpacing)), () => Data.Height, () => Data.ColumnSize);
        Wireframe.SquareColumn(new DynamicPoint(() => new Vector3(Data.ColumnSpacing, 0, Data.ColumnSpacing)), () => Data.Height, () => Data.ColumnSize);
        Wireframe.SquareColumn(new DynamicPoint(() => new Vector3(Data.ColumnSpacing, 0, -Data.ColumnSpacing)), () => Data.Height, () => Data.ColumnSize);

        // roof
        Wireframe.SquareColumn(new DynamicPoint(() => new Vector3(0, Data.Height, 0)), () => Data.RoofThickness, () => .5f);
        Wireframe.SquareColumn(new DynamicPoint(() => new Vector3(0, Data.Height + Data.RoofThickness, 0)), () => Data.RoofThickness, () => .4f);
    }

    protected override EndPortalMeshGeneratorData LoadData() => DataService.GetData<MeshGeneratorDataCollection>().EndPortal;
}
