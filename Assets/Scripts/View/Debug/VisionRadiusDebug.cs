using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(VisionRadius))]
public class VisionRadiusDebug : MonoBehaviour
{
    const int CIRCLE_SEGMENTS = 64;

    [SerializeField]
    LineRenderer _lineRenderer;

    VisionRadius _visionRadius;
    float _range=-1;

    private void Awake()
    {
        _visionRadius = GetComponent<VisionRadius>();
        _lineRenderer.positionCount = CIRCLE_SEGMENTS;
    }

    private void Update()
    {
        var model = Game.Model.Vision.GetItem(_visionRadius.Id);
        if (model == null || model.Range == _range)
        {
            return;
        }

        UpdateRange(model);
    }

    void UpdateRange(IVisionModel model)
    {
        _range = model.Range;

        var p = new Vector3[CIRCLE_SEGMENTS];
        Vector3 dir = Vector3.right;

        float step = 360f / CIRCLE_SEGMENTS;
        var ang = Quaternion.AngleAxis(step, Vector3.up);
        for (int i = 0; i < CIRCLE_SEGMENTS; i++)
        {
            p[i] = dir * _range;
            dir = ang * dir;
        }

        _lineRenderer.SetPositions(p);
    }
}
