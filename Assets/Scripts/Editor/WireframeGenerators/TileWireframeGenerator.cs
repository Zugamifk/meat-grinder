using MeshGenerator.Wireframes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshGenerator.Editor
{
    public class TileWireframeGenerator : WireframeGenerator<TileMeshGeneratorData>
    {
        TileModel _tile;
        public void SetTile(TileModel tile)
        {
            _tile = tile;
        }

        protected override void BuildWireframe(Wireframe wireframe, TileMeshGeneratorData data)
        {
            var h = _tile.Height * data.TileStepHeight;

            var p0 = new Point(-.5f, h, -.5f);
            var p1 = new Point(-.5f, h, .5f);
            var p2 = new Point(.5f, h, .5f);
            var p3 = new Point(.5f, h, -.5f);
            wireframe.ConnectLoop(p0, p1, p2, p3);

            if (_tile.Type == ETileType.Path)
            {
                GeneratePaths(wireframe, data);
            }

            GenerateWallWireFrames(wireframe, data, p0, p1, p2, p3);
        }

        void GeneratePaths(Wireframe wireframe, TileMeshGeneratorData data)
        {
            var r = data.PathWidth * .5f;
            var h = _tile.Height * data.TileStepHeight;
            var r0 = new Point(new Vector3(-r, h, -r));
            var r1 = new Point(new Vector3(-r, h, r));
            var r2 = new Point(new Vector3(r, h, r));
            var r3 = new Point(new Vector3(r, h, -r));

            if (_tile.NorthEdge.Type == ETileType.Path)
            {
                var p0 = new Point(new Vector3(-r, h, .5f));
                var p1 = new Point(new Vector3(r, h, .5f));
                wireframe.Connect(p0, r1);
                wireframe.Connect(p1, r2);
            }
            else
            {
                wireframe.Connect(r1, r2);
            }

            if (_tile.SouthEdge.Type == ETileType.Path)
            {
                var p0 = new Point(new Vector3(-r, h, -.5f));
                var p1 = new Point(new Vector3(r, h, -.5f));
                wireframe.Connect(p0, r0);
                wireframe.Connect(p1, r3);
            }
            else
            {
                wireframe.Connect(r0, r3);
            }

            if (_tile.EastEdge.Type == ETileType.Path)
            {
                var p0 = new Point(new Vector3(.5f, h, -r));
                var p1 = new Point(new Vector3(.5f, h, r));
                wireframe.Connect(p0, r3);
                wireframe.Connect(p1, r2);
            }
            else
            {
                wireframe.Connect(r3, r2);
            }

            if (_tile.WestEdge.Type == ETileType.Path)
            {
                var p0 = new Point(new Vector3(-.5f, h, r));
                var p1 = new Point(new Vector3(-.5f, h, -r));
                wireframe.Connect(p0, r1);
                wireframe.Connect(p1, r0);
            }
            else
            {
                wireframe.Connect(r1, r0);
            }
        }

        private void GenerateWallWireFrames(Wireframe wireframe, TileMeshGeneratorData data, IPoint p0, IPoint p1, IPoint p2, IPoint p3)
        {
            if (_tile.NorthEdge.Type == ETileType.Wall)
            {
                DrawWall(wireframe, data, p1, p2);
            }

            if (_tile.SouthEdge.Type == ETileType.Wall)
            {
                DrawWall(wireframe, data, p3, p0);
            }

            if (_tile.WestEdge.Type == ETileType.Wall)
            {
                DrawWall(wireframe, data, p0, p1);
            }

            if (_tile.EastEdge.Type == ETileType.Wall)
            {
                DrawWall(wireframe, data, p2, p3);
            }
        }

        void DrawWall(Wireframe wireframe, TileMeshGeneratorData data, IPoint cornerA, IPoint cornerB)
        {
            var wallInset = Vector3.Cross(cornerA.Position - cornerB.Position, Vector3.up).normalized * data.WallInset;

            var i0 = new Point(cornerA.Position + wallInset);
            var i1 = new Point(cornerB.Position + wallInset);
            var w0 = new Point(cornerA.Position + .5f * wallInset + Vector3.up * data.WallInset);
            var w1 = new Point(cornerB.Position + .5f * wallInset + Vector3.up * data.WallInset);
            var w2 = new Point(cornerA.Position + .5f * wallInset + Vector3.up * (1 - data.WallInset));
            var w3 = new Point(cornerB.Position + .5f * wallInset + Vector3.up * (1 - data.WallInset));
            var i2 = new Point(cornerA.Position + wallInset + Vector3.up);
            var i3 = new Point(cornerB.Position + wallInset + Vector3.up);
            wireframe.Connect(i0, i1);
            wireframe.Connect(i0, w0);
            wireframe.Connect(i1, w1);
            wireframe.Connect(w0, w1);
            wireframe.Connect(w0, w2);
            wireframe.Connect(w1, w3);
            wireframe.Connect(w2, w3);
            wireframe.Connect(w2, i2);
            wireframe.Connect(w3, i3);
            wireframe.Connect(i2, i3);
            wireframe.Connect(i2, new DynamicPoint(() => cornerA.Position + Vector3.up));
            wireframe.Connect(i3, new DynamicPoint(() => cornerB.Position + Vector3.up));
        }
    }
}
