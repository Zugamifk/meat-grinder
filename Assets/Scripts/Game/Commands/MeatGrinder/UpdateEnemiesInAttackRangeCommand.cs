using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetObjectsInVisionRangeCommand : ICommand
{
    Guid _visionId;
    HashSet<Guid> _objectIds;

    public SetObjectsInVisionRangeCommand(Guid visionId, HashSet<Guid> objectIds)
    {
        _visionId = visionId;
        _objectIds = objectIds;
    }

    public void Execute(GameModel model)
    {
        var vision = model.Vision.GetItem(_visionId);
        vision.ObjectsInRange = _objectIds;
    }
}
