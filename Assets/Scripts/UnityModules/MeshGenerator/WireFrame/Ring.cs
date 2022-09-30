using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshGenerator.Wireframe
{
    public class Ring
    {
        public IPoint Center;
        public Func<float> Radius;
        public Func<Vector3> Normal;
    }
}
