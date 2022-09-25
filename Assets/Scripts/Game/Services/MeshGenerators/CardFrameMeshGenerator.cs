using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator;
using MeshGenerator.Wireframe;
using System;

[MeshGenerator("Card Frame")]
public class CardFrameMeshGenerator : MeshGeneratorWithWireFrame<CardFrameMeshGeneratorData>
{
    public override void BuildWireframe()
    {
        Wireframe = new();

        Func<float> w = () => Data.BaseDimensions.x / 2;
        Func<float> h = () => Data.BaseDimensions.y / 2;
        var p0 = new DynamicPoint(() => new Vector3(-w(), -h(), 0));
        var p1 = new DynamicPoint(() => new Vector3(-w(), h(), 0));
        var p2 = new DynamicPoint(() => new Vector3(w(), h(), 0));
        var p3 = new DynamicPoint(() => new Vector3(w(), -h(), 0));

        Wireframe.ConnectLoop(p0, p1, p2, p3);

        Func<float> bw = () => w() + Data.BorderWidth;
        Func<float> bh = () => h() + Data.BorderWidth;
        var b0 = new DynamicPoint(() => new Vector3(-bw(), -bh(), 0));
        var b1 = new DynamicPoint(() => new Vector3(-bw(), bh(), 0));
        var b2 = new DynamicPoint(() => new Vector3(bw(), bh(), 0));
        var b3 = new DynamicPoint(() => new Vector3(bw(), -bh(), 0));
        Wireframe.ConnectLoop(b0, b1, b2, b3);

        Func<float> dw = () => Data.DividerWidth / 2;
        Func<float> d = () => Mathf.Lerp(dw(), Data.BaseDimensions.y - dw(), Data.DividerPosition);
        var d0 = new DynamicPoint(() => p0.Position + new Vector3(0, d() - dw(), 0));
        var d1 = new DynamicPoint(() => p3.Position + new Vector3(0, d() - dw(), 0));
        Wireframe.Connect(d0, d1);
        var d2 = new DynamicPoint(() => p0.Position + new Vector3(0, d() + dw(), 0));
        var d3 = new DynamicPoint(() => p3.Position + new Vector3(0, d() + dw(), 0));
        Wireframe.Connect(d2, d3);

        void AddBlock(IPoint p0, IPoint p1, IPoint p2, IPoint p3, IPoint p4, IPoint p5, IPoint p6, IPoint p7)
        {
            Wireframe.Connect(p0, p1);
            Wireframe.Connect(p1, p2);
            Wireframe.Connect(p2, p3);
            Wireframe.Connect(p3, p0);
            Wireframe.Connect(p0, p4);
            Wireframe.Connect(p1, p5);
            Wireframe.Connect(p2, p6);
            Wireframe.Connect(p3, p7);
            Wireframe.Connect(p4, p5);
            Wireframe.Connect(p5, p6);
            Wireframe.Connect(p6, p7);
            Wireframe.Connect(p7, p4);
        }

        var spacing = .2f;
        var f = new Vector3(0, 0, -.5f);
        var bl0 = new DynamicPoint(() => p0.Position + Vector3.up * spacing);
        var bl1 = new DynamicPoint(() => b0.Position + Vector3.up * spacing);
        var bl2 = new DynamicPoint(() => b1.Position - Vector3.up * spacing);
        var bl3 = new DynamicPoint(() => p1.Position - Vector3.up * spacing);
        var bl4 = new DynamicPoint(() => p0.Position + Vector3.up * spacing + f);
        var bl5 = new DynamicPoint(() => b0.Position + Vector3.up * spacing + f);
        var bl6 = new DynamicPoint(() => b1.Position - Vector3.up * spacing + f);
        var bl7 = new DynamicPoint(() => p1.Position - Vector3.up * spacing + f);
        AddBlock(bl0, bl1, bl2, bl3, bl4, bl5, bl6, bl7);
    }

    public override void Generate(MeshBuilder builder)
    {
        builder.SetColor(Color.white);

        var w = Data.BaseDimensions.x / 2;
        var h = Data.BaseDimensions.y / 2;
        var p0 = new Vector3(-w, -h, 0);
        var p1 = new Vector3(-w, h, 0);
        var p2 = new Vector3(w, h, 0);
        var p3 = new Vector3(w, -h, 0);
        builder.AddQuad(p0, p1, p2, p3);

        var bw = w + Data.BorderWidth;
        var bh = h + Data.BorderWidth;
        var b0 = new Vector3(-bw, -bh, 0);
        var b1 = new Vector3(-bw, bh, 0);
        var b2 = new Vector3(bw, bh, 0);
        var b3 = new Vector3(bw, -bh, 0);

        var f = new Vector3(0, 0, .5f);
        builder.AddCubic(p0 + f, b0 + f, b1 + f, p1 + f, p0, b0, b1, p1);
    }

    protected override CardFrameMeshGeneratorData LoadData() => DataService.GetData<MeshGeneratorDataCollection>().CardFrame;
}
