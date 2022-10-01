using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshGenerator
{
    [System.Serializable]
    public class ScriptedAnimationData
    {
        public AnimationCurve Curve;
        public float Duration = 1;
        public float Magnitude = 1;

        public IEnumerator Play(Action<float> binding)
        {
            for(float t=0; t<1;t+=Time.deltaTime/Duration)
            {
                var val = Curve.Evaluate(t) * Magnitude;
                binding(val);
                yield return null;
            }
        }
    }

}
