using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateEnemiesInRangeCommand : ICommand
{
    Guid _buildingId;
    HashSet<Guid> _ids;

    public UpdateEnemiesInRangeCommand(Guid buildingId, HashSet<Guid> ids)
    {
        _buildingId = buildingId;
        _ids = ids;
    }

    public void Execute(GameModel model)
    {
        throw new NotImplementedException();
    }
}
