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

    Guid _id;

    void Start()
    {
        _id = Guid.NewGuid();
        Game.Do(new CreateAICommand(_id));
        _vision.Id = _id;
    }

    private void Update()
    {
        var rad = Game.Model.Vision.GetItem(_id);
        if(_radius!=rad.Range)
        {
            Game.Do(new SetVisionRangeCommand(_id, _radius));
        }
    }
}
