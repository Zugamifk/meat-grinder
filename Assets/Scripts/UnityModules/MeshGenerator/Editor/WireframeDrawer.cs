using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator.Wireframe;
using UnityEditor;

namespace MeshGenerator.Editor
{
    public static class WireframeDrawer
    {
        public static void Draw(Frame frame)
        {
            foreach(var e in frame.Edges)
            {
                Handles.DrawLine(e.A.Position, e.B.Position);
            }

            foreach(var r in frame.Rings)
            {
                Handles.DrawWireDisc(r.Center.Position, r.Normal(), r.Radius());
            }
        }
    }
}
