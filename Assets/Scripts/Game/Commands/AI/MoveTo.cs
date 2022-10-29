using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class MoveTo : ICommand
    {
        Guid _id;
        Vector3 _destination;

        public MoveTo(Guid id, Vector3 destination)
        {
            _id = id;
            _destination = destination;
        }

        public void Execute(GameModel model)
        {
            var ai = model.AI.GetItem(_id);
            var behaviour = new MoveToBehaviourModel();
            behaviour.Destination = _destination;
            ai.Behaviour = behaviour;
        }
    }
}