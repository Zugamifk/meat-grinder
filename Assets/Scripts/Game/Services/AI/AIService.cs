using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class AIService
    {
        static Dictionary<Type, IAIBehaviour> _behaviourModelTypeToBehaviour = new Dictionary<Type, IAIBehaviour>();
        static ActionPlanner _actionplanner = new();

        public static void RegisterAction<TModel>(IAIBehaviour behaviour, PlannableAction action)
            where TModel : IAIBehaviourModel
        {
            _behaviourModelTypeToBehaviour[typeof(TModel)] = behaviour;
            _actionplanner.RegisterPlannableAction(action);
        }

        public void UpdateBehaviour(GameModel model, AIModel ai)
        {
            if (!_behaviourModelTypeToBehaviour.TryGetValue(ai.Behaviour.GetType(), out IAIBehaviour behaviour))
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