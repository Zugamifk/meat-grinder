using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI {
    public class MoveToBehaviour : IAIBehaviour
    {
        public void Update(GameModel model, AIModel ai)
        {
            var ship = model.Ships.GetItem(ai.Id);
            if (ship.Movement.TargetPosition != null)
            {
                return;
            }

            var behaviour = (MoveToBehaviourModel)ai.Behaviour;

            Game.Do(new SetShipDestinationCommand(ship.Id, behaviour.Destination));
        }
    }
}