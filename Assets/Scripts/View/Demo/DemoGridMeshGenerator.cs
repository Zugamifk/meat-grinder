using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshGenerator
{
    public class DemoGridMeshGenerator : MeshGenerator
    {
        public Vector2 Dimensions = new Vector2(10, 10);
        public Vector2Int Grid = new Vector2Int(10, 10);
        protected override void BuildMesh(MeshBuilder builder)
        {
            builder.SetColor(Colorx.FromHex(0x90AD6B));

            var w = Dimensions.x;
            var h = Dimensions.y;

            var gx = Grid.x;
            var gy = Grid.y;
            var xs = w / gx;
            var ys = h / gy;
            var x0 = -w / 2;
            var y0 = -h / 2;

            for (int x = 0; x < gx; x++)
            {
                for (int y = 0; y < gy; y++)
                {
                    builder.AddQuad(new Vector3(x0 + x * xs, 0, y0 + y * ys),
                        new Vector3(x0 + x * xs, 0, y0 + (y + 1) * ys),
                        new Vector3(x0 + (x + 1) * xs, 0, y0 + (y + 1) * ys),
                        new Vector3(x0 + (x + 1) * xs, 0, y0 + y * ys));
                }
            }
        }
    }
}