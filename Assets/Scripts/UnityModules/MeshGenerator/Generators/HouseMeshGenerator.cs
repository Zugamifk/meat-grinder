using MeshGenerator.Wireframe;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshGenerator
{
    [MeshGenerator("House")]
    public class HouseGenerator : MeshGenerator
    {
        public class GeometryData : ScriptableObject
        {
            public float Rotation;

            public Vector2 FloorDimensions;

            public float BaseExtents = 1;
            public float Height = 3;

            public float RoofPeak = 2;
            public float EavesLength = 1;
            public float WindowHeight = 1;

            [Serializable]
            public class DoorData
            {
                public float Position = .5f;
                public Vector2 Dimensions = Vector2.one;
                public int Wall = 0;
            }

            [Serializable]
            public class WindowData
            {
                public float Position;
                public Vector2 Dimensions = Vector2.one;
            }

            [Serializable]
            public class WallData
            {
                public List<WindowData> Windows = new();
            }
            public List<WallData> Walls = new();

            public DoorData Door;

            public static GeometryData Instance;
            private void OnEnable()
            {
                if (Walls.Count != 4)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Walls.Add(new());
                    }
                }

                Instance = this;
            }
        }

        class WallSectionPointData
        {
            public List<IPoint> BoundPoints = new();
            public List<IPoint> Intervals = new();
        }

        // wireframe points
        List<IPoint> _basePoints = new();
        List<WallSectionPointData> _walls = new();
        List<IPoint> _roofPoints = new();
        List<IPoint> _atticWallPoints = new();

        public GeometryData Data => _data;
        public Frame Wireframe => _wireframe;

        static GeometryData _data => GeometryData.Instance;

        Frame _wireframe;
        IPoint[] _wallCorners;

        static HouseGenerator()
        {
            if (_data == null)
            {
                ScriptableObject.CreateInstance<GeometryData>();
                _data.hideFlags = HideFlags.HideAndDontSave;
            }
        }

        protected override MeshGeneratorResult BuildMesh()
        {
            //base
            _builder.AddQuad(_basePoints[0].Position, _basePoints[1].Position, _basePoints[2].Position, _basePoints[3].Position);

            //walls
            _builder.AddTriangle(_atticWallPoints[0].Position, _atticWallPoints[1].Position, _atticWallPoints[2].Position);
            _builder.AddTriangle(_atticWallPoints[3].Position, _atticWallPoints[4].Position, _atticWallPoints[5].Position);

            //roof
            _builder.AddQuad(_roofPoints[0].Position, _roofPoints[1].Position, _roofPoints[4].Position, _roofPoints[5].Position);
            _builder.AddQuad(_roofPoints[2].Position, _roofPoints[1].Position, _roofPoints[4].Position, _roofPoints[3].Position);
            _builder.AddQuad(_roofPoints[0].Position, _roofPoints[5].Position, _roofPoints[4].Position, _roofPoints[1].Position);
            _builder.AddQuad(_roofPoints[2].Position, _roofPoints[3].Position, _roofPoints[4].Position, _roofPoints[1].Position);

            var result = new MeshGeneratorResult();
            result.Mesh = _builder.BuildMesh();
            return result;
        }

        public void BuildWireframe()
        {
            _wireframe = new();
            var d = Data;

            // base
            float fx() => d.FloorDimensions.x / 2 + d.BaseExtents;
            float fy() => d.FloorDimensions.y / 2 + d.BaseExtents;

            _basePoints.Clear();
            _basePoints.Add(new DynamicPoint(() => new Vector3(-fx(), 0, -fy())));
            _basePoints.Add(new DynamicPoint(() => new Vector3(-fx(), 0, fy())));
            _basePoints.Add(new DynamicPoint(() => new Vector3(fx(), 0, fy())));
            _basePoints.Add(new DynamicPoint(() => new Vector3(fx(), 0, -fy())));

            _wireframe.Connect(_basePoints[0], _basePoints[1]);
            _wireframe.Connect(_basePoints[1], _basePoints[2]);
            _wireframe.Connect(_basePoints[2], _basePoints[3]);
            _wireframe.Connect(_basePoints[3], _basePoints[0]);

            // walls
            var h = new Vector3(0, d.Height, 0);
            float bx() => d.FloorDimensions.x / 2;
            float by() => d.FloorDimensions.y / 2;
            var w0 = new DynamicPoint(() => new Vector3(-bx(), 0, -by()));
            var w1 = new DynamicPoint(() => new Vector3(-bx(), 0, by()));
            var w2 = new DynamicPoint(() => new Vector3(bx(), 0, by()));
            var w3 = new DynamicPoint(() => new Vector3(bx(), 0, -by()));
            _wallCorners = new[] { w0, w1, w2, w3 };

            _wireframe.Connect(w0, w1);
            _wireframe.Connect(w1, w2);
            _wireframe.Connect(w2, w3);
            _wireframe.Connect(w3, w0);

            var w4 = new DynamicPoint(() => w0.Position + Vector3.up * d.Height);
            var w5 = new DynamicPoint(() => Vector3.Lerp(w0.Position, w1.Position, .5f) + Vector3.up * (d.Height + d.RoofPeak));
            var w6 = new DynamicPoint(() => w1.Position + Vector3.up * d.Height);
            var w7 = new DynamicPoint(() => w2.Position + Vector3.up * d.Height);
            var w8 = new DynamicPoint(() => Vector3.Lerp(w2.Position, w3.Position, .5f) + Vector3.up * (d.Height + d.RoofPeak));
            var w9 = new DynamicPoint(() => w3.Position + Vector3.up * d.Height);

            _wireframe.Connect(w0, w4);
            _wireframe.Connect(w1, w6);
            _wireframe.Connect(w2, w7);
            _wireframe.Connect(w3, w9);

            _wireframe.Connect(w4, w5);
            _wireframe.Connect(w5, w6);
            _wireframe.Connect(w6, w7);
            _wireframe.Connect(w7, w8);
            _wireframe.Connect(w8, w9);
            _wireframe.Connect(w9, w4);

            _atticWallPoints = new() { w5, w4, w6, w8, w7, w9 };

            

            // roof
            Vector3 rd() => (w2.Position - w1.Position).normalized;
            Vector3 rdl() => (w4.Position - w5.Position).normalized;
            Vector3 rdr() => (w6.Position - w5.Position).normalized;

            var r0 = new DynamicPoint(() => w4.Position - rd() * d.EavesLength + rdl() * d.EavesLength);
            var r1 = new DynamicPoint(() => w5.Position - rd() * d.EavesLength);
            var r2 = new DynamicPoint(() => w6.Position - rd() * d.EavesLength + rdr() * d.EavesLength);
            var r3 = new DynamicPoint(() => w7.Position + rd() * d.EavesLength + rdr() * d.EavesLength);
            var r4 = new DynamicPoint(() => w8.Position + rd() * d.EavesLength);
            var r5 = new DynamicPoint(() => w9.Position + rd() * d.EavesLength + rdl() * d.EavesLength);

            _wireframe.Connect(r0, r1);
            _wireframe.Connect(r1, r2);
            _wireframe.Connect(r2, r3);
            _wireframe.Connect(r3, r4);
            _wireframe.Connect(r4, r5);
            _wireframe.Connect(r5, r0);
            _wireframe.Connect(r1, r4);

            _roofPoints = new() { r0, r1, r2, r3, r4, r5 };

            // windows
            for (int i = 0; i < 4; i++)
            {
                var w = d.Walls[i];
                var wp0 = _wallCorners[i];
                var wp1 = _wallCorners[(i + 1) % 4];
                Func<Vector3> wd = () =>
                {
                    return (wp1.Position - wp0.Position).normalized;
                };

                for (int j = 0; j < w.Windows.Count; j++)
                {
                    var window = w.Windows[j];
                    var ww0 = new DynamicPoint(() => Vector3.Lerp(wp1.Position - wd() * window.Dimensions.x, wp0.Position, window.Position) + Vector3.up * d.WindowHeight);
                    var ww1 = new DynamicPoint(() => ww0.Position + Vector3.up * window.Dimensions.y);
                    var ww2 = new DynamicPoint(() => ww0.Position + Vector3.up * window.Dimensions.y + wd() * window.Dimensions.x);
                    var ww3 = new DynamicPoint(() => ww0.Position + wd() * window.Dimensions.x);

                    _wireframe.Connect(ww0, ww1);
                    _wireframe.Connect(ww1, ww2);
                    _wireframe.Connect(ww2, ww3);
                    _wireframe.Connect(ww3, ww0);
                }
            }

            // door
            Func<Vector3> dir = () =>
            {
                var di = d.Door.Wall;
                var wp0 = _wallCorners[di].Position;
                var wp1 = _wallCorners[(di + 1) % 4].Position;
                return (wp1 - wp0).normalized;
            };
            var d0 = new DynamicPoint(() =>
            {
                var di = d.Door.Wall;
                var wp0 = _wallCorners[di].Position;
                var wp1 = _wallCorners[(di + 1) % 4].Position;
                return Vector3.Lerp(wp1 - dir() * d.Door.Dimensions.x, wp0, d.Door.Position);
            });
            var d1 = new DynamicPoint(() => d0.Position + Vector3.up * d.Door.Dimensions.y);
            var d2 = new DynamicPoint(() => d1.Position + dir() * d.Door.Dimensions.x);
            var d3 = new DynamicPoint(() => d0.Position + dir() * d.Door.Dimensions.x);

            _wireframe.Connect(d0, d1);
            _wireframe.Connect(d1, d2);
            _wireframe.Connect(d2, d3);
        }
    }
}
