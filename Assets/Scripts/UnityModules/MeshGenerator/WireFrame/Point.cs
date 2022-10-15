using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshGenerator.Wireframes
{
    public class Point : IPoint
    {
        public Vector3 Position { get; set; }
        public Point() { }
        public Point(Vector3 position) => Position = position;
        public Point(float x, float y, float z) => Position = new Vector3(x, y, z);
    }
}
