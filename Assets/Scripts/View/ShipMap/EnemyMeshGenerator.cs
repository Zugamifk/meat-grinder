using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator;
using MeshGenerator.Wireframes;

[MeshGenerator("Enemy")]
public class EnemyMeshGenerator : MeshGeneratorWithData<EnemyMeshGeneratorData>
{
    protected override void BuildMesh(MeshBuilder builder)
    {
        var f = Data.Fatness;
        var p0 = new Vector3(-f, 0, -f);
        var p1 = new Vector3(-f, 0, f);
        var p2 = new Vector3(f, 0, f);
        var p3 = new Vector3(f, 0, -f);
        var h = Data.Height;
        var p4 = p0 + new Vector3(0, h, 0);
        var p5 = p1 + new Vector3(0, h, 0);
        var p6 = p2 + new Vector3(0, h, 0);
        var p7 = p3 + new Vector3(0, h, 0);

        builder.SetColor(Data.BodyColor);
        builder.AddQuad(p3, p2, p1, p0);
        builder.AddQuad(p0, p1, p5, p4);
        builder.AddQuad(p1, p2, p6, p5);
        builder.AddQuad(p2, p3, p7, p6);
        builder.AddQuad(p3, p0, p4, p7);
        builder.AddQuad(p4, p5, p6, p7);

        var hs = Data.HeadSize;
        p0 = new Vector3(-hs, h, -hs);
        p1 = new Vector3(-hs, h, hs);
        p2 = new Vector3(hs, h, hs);
        p3 = new Vector3(hs, h, -hs);
        var hh = Data.HeadHeight;
        p4 = p0 + new Vector3(0, hh, 0);
        p5 = p1 + new Vector3(0, hh, 0);
        p6 = p2 + new Vector3(0, hh, 0);
        p7 = p3 + new Vector3(0, hh, 0);

        builder.SetColor(Data.SkinColor);
        builder.AddQuad(p3, p2, p1, p0);
        builder.AddQuad(p0, p1, p5, p4);
        builder.AddQuad(p1, p2, p6, p5);
        builder.AddQuad(p2, p3, p7, p6);
        builder.AddQuad(p3, p0, p4, p7);
        builder.AddQuad(p4, p5, p6, p7);
    }

    protected override EnemyMeshGeneratorData LoadData() => DataService.GetData<MeshGeneratorDataCollection>().Enemy;
}
