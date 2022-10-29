using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(VisionRadius))]
public class VisionRadiusDebug : MonoBehaviour
{
    const int CIRCLE_SEGMENTS = 64;

    [SerializeField]
    LineRenderer _lineRenderer;
    [SerializeField]
    LineRenderer _targetLine;

    VisionRadius _visionRadius;
    float _range=-1;
    Stack<LineRenderer> _pool = new();
    Dictionary<Guid, LineRenderer> _activeLines = new();
    List<Guid> _toRemove = new();

    private void Awake()
    {
        _visionRadius = GetComponent<VisionRadius>();
        _lineRenderer.positionCount = CIRCLE_SEGMENTS;
        _pool.Push(_targetLine);
    }

    private void Update()
    {
        var model = Game.Model.Vision.GetItem(_visionRadius.Id);
        if (model == null)
        {
            return;
        }

        if(model.Range != _range)
        {
            UpdateRange(model);
        }

        UpdateLines(model);
    }

    void UpdateLines(IVisionModel model)
    {
        _toRemove.Clear();
        foreach (var id in _activeLines.Keys)
        {
            if(!model.ObjectsInRange.Contains(id))
            {
                var l = _activeLines[id];
                l.gameObject.SetActive(false);
                _pool.Push(l);
                _toRemove.Add(id);
            }
        }

        foreach(var id in _toRemove)
        {
            _activeLines.Remove(id);
        }

        foreach(var id in model.ObjectsInRange)
        {
            if(!_activeLines.TryGetValue(id, out LineRenderer l))
            {
                if(_pool.Count > 0)
                {
                    l = _pool.Pop();
                } else
                {
                    l = Instantiate(_targetLine);
                    l.transform.parent = _targetLine.transform.parent;
                }
                l.gameObject.SetActive(true);
                _activeLines[id] = l;
            }

            var go = ViewLookup.Get(id);
            l.SetPositions(new Vector3[] { Vector3.zero, go.transform.position });
            l.positionCount = 2;
        }
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
