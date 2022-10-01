using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshGenerator.Skeleton
{
    public class MeshSkeleton : MonoBehaviour
    {
        [SerializeField]
        MeshBone[] _bones;

        public IReadOnlyList<MeshBone> Bones => _bones;
    }
}
