using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class AIService
    {
        static Dictionary<Type, IAIBehaviour> _keyToBehaviour = new Dictionary<Type, IAIBehaviour>();

        static AIService()
        {
            _keyToBehaviour[typeof(PatrolBehaviourModel)] = new PatrolBehaviour();
            _keyToBehaviour[typeof(MoveToBehaviourModel)] = new MoveToBehaviour();
        }

        public void UpdateBehaviour(GameModel model, AIModel ai)
        {
            if (!_keyToBehaviour.TryGetValue(ai.Behaviour.GetType(), out IAIBehaviour behaviour))
            {
                throw new ArgumentException($"No bbehaviour for AIBEhaviourModel type {ai.Behaviour.GetType()}");
                return;
            }

            behaviour.Update(model, ai);
        }
    }
}