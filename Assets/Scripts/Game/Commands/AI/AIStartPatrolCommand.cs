using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

public class AIStartPatrolCommand : ICommand
{
    Guid _id;
    Vector3 _targetPosition;
    Vector3? _otherPosition;
    public AIStartPatrolCommand(Guid id, Vector3 targetPosition, Vector3? otherPosition = null)
    {
        _id = id;
        _targetPosition = targetPosition;
        _otherPosition = otherPosition;
    }

    public void Execute(GameModel model)
    {
        var ai = model.AI.GetItem(_id);
        var ship = model.Ships.GetItem(ai.Id);
        var behaviour = new PatrolBehaviourModel();
        behaviour.Waypoints.Clear();
        var start = _otherPosition.HasValue ? _otherPosition.Value : ship.Movement.CurrentPosition;
        behaviour.Waypoints.Add(start);
        behaviour.Waypoints.Add(_targetPosition);
        ai.Behaviour = behaviour;
    }
}
