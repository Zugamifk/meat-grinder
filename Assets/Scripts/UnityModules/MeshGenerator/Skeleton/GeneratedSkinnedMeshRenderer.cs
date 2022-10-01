using MeshGenerator.Skeleton;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

namespace MeshGenerator
{
    [RequireComponent(typeof(SkinnedMeshRenderer))]
    [RequireComponent(typeof(MeshSkeleton))]
    public class GeneratedSkinnedMeshRenderer : MonoBehaviour
    {
        MeshSkeleton _skeleton_cached;
        MeshSkeleton _skeleton => _skeleton_cached ??= GetComponent<MeshSkeleton>();

        SkinnedMeshRenderer _meshRenderer_cached;
        SkinnedMeshRenderer _meshRenderer => _meshRenderer_cached ??= GetComponent<SkinnedMeshRenderer>();

        Transform[] GetBones()
        {
            var bones = _skeleton.Bones;
            var boneTfs = new Transform[bones.Count];
            for (int i = 0; i < bones.Count; i++)
            {
                boneTfs[i] = bones[i].transform;
            }
            return boneTfs;
        }

        public void ApplyMesh(Mesh mesh)
        {
            _meshRenderer.bones = GetBones();
            SetBindPoses(mesh);
            _meshRenderer.sharedMesh = mesh;
        }

        void SetBindPoses(Mesh mesh)
        {
            var bindPoses = new Matrix4x4[_skeleton.Bones.Count];
            for (int i = 0;i < _skeleton.Bones.Count; i++)
            {
                bindPoses[i] = _skeleton.Bones[i].GetBindPose();
            }
            mesh.bindposes = bindPoses;
        }

        public void Clear()
        {
            _meshRenderer.sharedMesh = null;
        }
    }
}
