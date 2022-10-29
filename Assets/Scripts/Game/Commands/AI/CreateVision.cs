using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateVision : ICommand
{
    Guid _id;
    float _range;

    public CreateVision(Guid id, float range)
    {
        _id = id;
        _range = range;
    }

    public void Execute(GameModel model)
    {
        var vision = new VisionModel()
        {
            Id = _id,
            Range = _range
        };
        model.Vision.AddItem(vision);
    }
}
