using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIService
{
    static Dictionary<Type, IAIBehaviour> _keyToBehaviour = new Dictionary<Type, IAIBehaviour>();

    static AIService()
    {
        _keyToBehaviour[typeof(AIPatrolBehaviourModel)] = new AIPatrolBehaviour();
    }

    public void UpdateBehaviour(GameModel model, AIModel ai)
    {
        if(!_keyToBehaviour.TryGetValue(ai.Behaviour.GetType(), out IAIBehaviour behaviour))
        {
            return;
        }

        behaviour.Update(model, ai);
    }
}
