using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrolBehaviour : AIBehaviour
{
    public override void Update(GameModel model, AIModel ai)
    {
        var ship = model.Ships.GetItem(ai.Id);
        if(ship.Movement.TargetPosition != null)
        {
            return;
        }

        var bvr = ai.Behaviour;
        bvr.CurrentWaypointIndex = (bvr.CurrentWaypointIndex + 1) % bvr.Waypoints.Count;

        Game.Do(new SetShipDestinationCommand(ship.Id, bvr.Waypoints[bvr.CurrentWaypointIndex]));
    }
}
