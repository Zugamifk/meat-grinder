using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshGenerator.Skeleton
{
    [RequireComponent(typeof(SkinnedMeshRenderer))]
    public class MeshSkeleton : MonoBehaviour
    {
        [SerializeField]
        MeshBone[] _bones;

        SkinnedMeshRenderer _meshRenderer_cached;
        SkinnedMeshRenderer _meshRenderer => _meshRenderer_cached ??= GetComponent<SkinnedMeshRenderer>();

        public IReadOnlyList<MeshBone> Bones => _bones;
        
        private void Start()
        {
            _meshRenderer.bones = GetBones();
        }

        Transform[] GetBones()
        {
            var bones = new Transform[_bones.Length];
            for(int i=0;i<_bones.Length;i++)
            {
                bones[i] = _bones[i].transform;
            }
            return bones;
        }

        public void ApplyMesh(Mesh mesh)
        {
            _meshRenderer.sharedMesh = mesh;
        }

        public void Clear()
        {
            _meshRenderer.sharedMesh = null;
        }
    }
}
