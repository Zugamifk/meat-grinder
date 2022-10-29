using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionDemo : MonoBehaviour
{
    [SerializeField]
    VisionRadius _vision;
    [SerializeField]
    float _radius;
    [SerializeField]
    Transform _target;
    [SerializeField]
    Transform _targetPathStart;
    [SerializeField]
    Transform _targetPathEnd;
    [SerializeField]
    float _moveSpeed;

    Guid _id;
    Transform[] _targets;
    float _offset = 0;

    private void Awake()
    {
        _targets = new Transform[10];
        _targets[0] = _target;
        for (int i = 1; i < 10; i++)
        {
            var t = Instantiate(_target);
            t.parent = _target.parent;
            _targets[i] = t;
        }
    }

    void Start()
    {
        _id = Guid.NewGuid();
        Game.Do(new CreateAI(_id));
        _vision.Id = _id;
    }

    private void Update()
    {
        var rad = Game.Model.Vision.GetItem(_id);
        if (_radius != rad.Range)
        {
            Game.Do(new SetVisionRange(_id, _radius));
        }

        var to = (_targetPathEnd.position - _targetPathStart.position);
        var dir = to.normalized;
        var dist = to.magnitude;
        var step = dist / 10;
        _offset = (_offset + _moveSpeed * Time.deltaTime) % dist;
        var itemOffset = _offset;
        for (int i = 0; i < 10; i++)
        {
            _targets[i].transform.position = _targetPathStart.position + dir * itemOffset;
            itemOffset = (itemOffset + step) % dist;
        }
    }
}
