using MeshGenerator.Wireframes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshGenerator.Editor
{
    public class EndPortalWireframeGenerator : WireframeGenerator<EndPortalMeshGeneratorData>
    {
        protected override void BuildWireframe(Wireframe wireframe, EndPortalMeshGeneratorData data)
        {
            var b = .5f;
            var b0 = new Vector3(-b, 0, -b);
            var b1 = new Vector3(-b, 0, b);
            var b2 = new Vector3(b, 0, b);
            var b3 = new Vector3(b, 0, -b);

            // base
            wireframe.Connect(b0, b1);
            wireframe.Connect(b1, b2);
            wireframe.Connect(b2, b3);
            wireframe.Connect(b3, b0);

            // columns
            wireframe.SquareColumn(new Vector3(-data.ColumnSpacing, 0, -data.ColumnSpacing), data.Height, data.ColumnSize);
            wireframe.SquareColumn(new Vector3(-data.ColumnSpacing, 0, data.ColumnSpacing), data.Height, data.ColumnSize);
            wireframe.SquareColumn(new Vector3(data.ColumnSpacing, 0, data.ColumnSpacing), data.Height, data.ColumnSize);
            wireframe.SquareColumn(new Vector3(data.ColumnSpacing, 0, -data.ColumnSpacing), data.Height, data.ColumnSize);

            // roof
            wireframe.SquareColumn(new Vector3(0, data.Height, 0), data.RoofThickness, .5f);
            wireframe.SquareColumn(new Vector3(0, data.Height + data.RoofThickness, 0), data.RoofThickness, .4f);
        }
    }
}
