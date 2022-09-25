using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshGenerator.Wireframe
{
    public interface IWireframeUser
    {
        Frame Wireframe { get; }
        void BuildWireframe();
    }
}
