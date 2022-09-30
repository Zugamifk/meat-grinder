using MeshGenerator.Skeleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshGenerator
{
    [RequireComponent(typeof(MeshSkeleton))]
    public class GeneratedSkinnedMeshRenderer : MonoBehaviour
    {
        MeshSkeleton _skeleton_cached;
        MeshSkeleton _skeleton => _skeleton_cached ??= GetComponent<MeshSkeleton>();
        
        public void ApplyMesh(Mesh mesh)
        {
            _skeleton.ApplyMesh(mesh);
        }

        public void Clear()
        {
            _skeleton.Clear();
        }
    }
}
