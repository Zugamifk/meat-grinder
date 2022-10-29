using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class AIBehaviourUpdater : IUpdater
    {
        static AIService _aiService = new();

        Guid _id;
        public AIBehaviourUpdater(Guid id)
        {
            _id = id;
        }

        public void Update(GameModel model)
        {
            var ai = model.AI.GetItem(_id);
            if (ai.Behaviour != null)
            {
                _aiService.UpdateBehaviour(model, ai);
            }
        }
    }
}