using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace MeshGenerator.Wireframe
{
    public class Frame
    {
        public List<IPoint> Points = new();
        public List<Edge> Edges = new();
        public List<Ring> Rings = new();

        public void Connect(IPoint a, IPoint b)
        {
            Edges.Add(new Edge() { A = a, B = b });
        }

        public void Connect(params IPoint[] points)
        {
            for (int i = 1; i < points.Length; i++)
            {
                Connect(points[i - 1], points[i]);
            }
        }

        public void ConnectLoop(params IPoint[] points)
        {
            Connect(points);
            Connect(points[0], points[points.Length - 1]);
        }

        public void Prism(IPoint baseCentre, Func<float> height, int sides, Func<float> radius, Vector3 direction)
        {
            var rot = Quaternion.identity;
            var step = Quaternion.AngleAxis(360 / (float)sides, direction);
            Func<Vector3> baseDir = () => Vector3.Cross(new Vector3(.5f, 0, .5f), direction).normalized * radius();
            for (int i = 0; i < sides; i++)
            {
                var cr = rot;
                Func<Vector3> p0 = () => baseCentre.Position + cr * baseDir();
                Func<Vector3> p1 = () => baseCentre.Position + step * cr * baseDir();
                Connect(new DynamicPoint(p0), new DynamicPoint(p1));
                Func<Vector3> p2 = () => p0() + direction * height();
                Connect(new DynamicPoint(p0), new DynamicPoint(p2));
                Func<Vector3> p3 = () => p1() + direction * height();
                Connect(new DynamicPoint(p2), new DynamicPoint(p3));
                rot *= step;
            }
        }

        public void Prism(IList<IPoint> points, Func<float> height)
        {
            Func<Vector3> n = () => Vector3.Cross((points[0].Position - points[1].Position).normalized, (points[2].Position - points[1].Position).normalized);
            for(int i = 0; i <points.Count;i++)
            {
                var p0 = points[i];
                var p1 = points[(i + 1) % points.Count];
                Connect(p0, p1);
                var p2 = new DynamicPoint(() => p0.Position + n() * height());
                var p3 = new DynamicPoint(() => p1.Position + n() * height());
                Connect(p2, p3);
                Connect(p0, p2);
            }
        }

        public void SquareColumn(IPoint baseCentre, Func<float> height, Func<float> size)
        {
            var p0 = new DynamicPoint(() => baseCentre.Position + new Vector3(-size(), 0, -size()));
            var p1 = new DynamicPoint(() => baseCentre.Position + new Vector3(-size(), 0, size()));
            var p2 = new DynamicPoint(() => baseCentre.Position + new Vector3(size(), 0, size()));
            var p3 = new DynamicPoint(() => baseCentre.Position + new Vector3(size(), 0, -size()));
            var p4 = new DynamicPoint(() => baseCentre.Position + new Vector3(-size(), height(), -size()));
            var p5 = new DynamicPoint(() => baseCentre.Position + new Vector3(-size(), height(), size()));
            var p6 = new DynamicPoint(() => baseCentre.Position + new Vector3(size(), height(), size()));
            var p7 = new DynamicPoint(() => baseCentre.Position + new Vector3(size(), height(), -size()));

            Connect(p0, p1);
            Connect(p1, p2);
            Connect(p2, p3);
            Connect(p3, p0);

            Connect(p0, p4);
            Connect(p1, p5);
            Connect(p2, p6);
            Connect(p3, p7);

            Connect(p4, p5);
            Connect(p5, p6);
            Connect(p6, p7);
            Connect(p7, p4);
        }

        public void Cuboid(Func<Matrix4x4> transform, Func<float> width, Func<float> height, Func<float> depth)
        {
            var p0 = new DynamicPoint(() => transform().MultiplyPoint3x4(new Vector3(-width(), -height(), -depth())));
            var p1 = new DynamicPoint(() => transform().MultiplyPoint3x4(new Vector3(-width(), -height(), depth())));
            var p2 = new DynamicPoint(() => transform().MultiplyPoint3x4(new Vector3(width(), -height(), depth())));
            var p3 = new DynamicPoint(() => transform().MultiplyPoint3x4(new Vector3(width(), -height(), -depth())));
            var p4 = new DynamicPoint(() => transform().MultiplyPoint3x4(new Vector3(-width(), height(), -depth())));
            var p5 = new DynamicPoint(() => transform().MultiplyPoint3x4(new Vector3(-width(), height(), depth())));
            var p6 = new DynamicPoint(() => transform().MultiplyPoint3x4(new Vector3(width(), height(), depth())));
            var p7 = new DynamicPoint(() => transform().MultiplyPoint3x4(new Vector3(width(), height(), -depth())));

            Connect(p0, p1);
            Connect(p1, p2);
            Connect(p2, p3);
            Connect(p3, p0);

            Connect(p0, p4);
            Connect(p1, p5);
            Connect(p2, p6);
            Connect(p3, p7);

            Connect(p4, p5);
            Connect(p5, p6);
            Connect(p6, p7);
            Connect(p7, p4);
        }

        public void Cylinder(Func<Vector3> baseCentre, Func<float> radius, Func<float> length, Func<Vector3> normal, Func<Vector3> edgeTangent)
        {
            Rings.Add(new Ring()
            {
                Center = new DynamicPoint(baseCentre),
                Radius = radius,
                Normal = normal
            });

            Rings.Add(new Ring()
            {
                Center = new DynamicPoint(() => baseCentre() + normal() * length()),
                Radius = radius,
                Normal = normal
            });

            Func<Vector3> edge = () => Vector3.Cross(edgeTangent(), normal()).normalized;
            var p0 = new DynamicPoint(() => baseCentre() + edge() * radius());
            var p1 = new DynamicPoint(() => baseCentre() + -edge() * radius());
            var p2 = new DynamicPoint(() => baseCentre() + edge() * radius() + normal() * length());
            var p3 = new DynamicPoint(() => baseCentre() + -edge() * radius() + normal() * length());
            Connect(p0, p2);
            Connect(p1, p3);
        }
    }
}
