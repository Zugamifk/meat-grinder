using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class AIService
    {
        static Dictionary<Type, IAIBehaviour> _keyToBehaviour = new Dictionary<Type, IAIBehaviour>();
        static ActionPlanner _actionplanner = new();

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
            }

            behaviour.Update(model, ai);
        }

        public void Plan(GameModel model, AIModel ai, State currentState, State goal)
        {
            ai.Plan = _actionplanner.GetPlan(model, ai, currentState, goal);
        }
    }
}