using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshGenerator.Wireframe
{
    public class DynamicPoint : IPoint
    {
        public System.Func<Vector3> PointGetter;
        public Vector3 Position => PointGetter.Invoke();
        public DynamicPoint(System.Func<Vector3> pointGetter) => PointGetter = pointGetter;
    }
}
