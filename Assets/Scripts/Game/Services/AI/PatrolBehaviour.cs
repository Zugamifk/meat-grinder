using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class PatrolBehaviour : IAIBehaviour
    {
        public void Update(GameModel model, AIModel ai)
        {
            var ship = model.Ships.GetItem(ai.Id);
            if (ship.Movement.TargetPosition != null)
            {
                return;
            }

            var behaviour = (PatrolBehaviourModel)ai.Behaviour;
            behaviour.CurrentWaypointIndex = (behaviour.CurrentWaypointIndex + 1) % behaviour.Waypoints.Count;

            Game.Do(new SetShipDestinationCommand(ship.Id, behaviour.Waypoints[behaviour.CurrentWaypointIndex]));
        }
    }
}