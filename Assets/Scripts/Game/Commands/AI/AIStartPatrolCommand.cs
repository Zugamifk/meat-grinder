using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStartPatrolCommand : ICommand
{
    Guid _id;
    Vector3 _targetPosition;
    public AIStartPatrolCommand(Guid id, Vector3 targetPosition)
    {
        _id = id;
        _targetPosition = targetPosition;
    }

    public void Execute(GameModel model)
    {
        var ai = model.AI.GetItem(_id);
        var ship = model.Ships.GetItem(ai.AgentId);
        ai.Behaviour.Waypoints.Clear();
        ai.Behaviour.Waypoints.Add(ship.Movement.CurrentPosition);
        ai.Behaviour.Waypoints.Add(_targetPosition);
        ai.Behaviour.Key = AIBehaviours.PATROL;
    }
}
