using AnimationGenerator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTurretAnimator : ScriptedAnimation
{
    protected override void UpdateAnimation(Transform bone, float x)
    {
        bone.transform.localPosition = new Vector3(0, 0, -x);
    }
}
