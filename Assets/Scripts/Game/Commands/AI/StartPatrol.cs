using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class StartPatrol : ICommand
    {
        Guid _id;
        Vector3 _targetPosition;
        Vector3? _otherPosition;
        public StartPatrol(Guid id, Vector3 targetPosition, Vector3? otherPosition = null)
        {
            _id = id;
            _targetPosition = targetPosition;
            _otherPosition = otherPosition;
        }

        public void Execute(GameModel model)
        {
            var ai = model.AI.GetItem(_id);
            var behaviour = new PatrolBehaviourModel();
            behaviour.Waypoints.Clear();
            Vector3 start;
            if (_otherPosition.HasValue)
            {
                start = _otherPosition.Value;
            }
            else
            {
                var ship = model.Ships.GetItem(ai.Id);
                start = ship.Movement.CurrentPosition;
            }
            behaviour.Waypoints.Add(start);
            behaviour.Waypoints.Add(_targetPosition);
            ai.Behaviour = behaviour;
        }
    }
}