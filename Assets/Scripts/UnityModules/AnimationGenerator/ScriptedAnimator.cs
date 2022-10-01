using MeshGenerator;
using MeshGenerator.Skeleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimationGenerator
{
    public abstract class ScriptedAnimation : MonoBehaviour
    {
        [SerializeField] MeshSkeleton _skeleton;

        public void PlayAnimation(string boneKey, string value, ScriptedAnimationData data)
        {
            var bone = _skeleton.GetBone(boneKey);
            StartCoroutine(data.Play(x=>UpdateAnimation(bone.transform, x)));
        }

        protected abstract void UpdateAnimation(Transform bone, float x);
    }
}
