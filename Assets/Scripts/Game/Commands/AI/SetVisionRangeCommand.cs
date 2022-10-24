using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVisionRangeCommand : ICommand
{
    Guid _id;
    float _range;

    public SetVisionRangeCommand(Guid id, float range)
    {
        _id = id;
        _range = range;
    }

    public void Execute(GameModel model)
    {
        var vision = model.Vision.GetItem(_id);
        vision.Range = _range;
    }
}
