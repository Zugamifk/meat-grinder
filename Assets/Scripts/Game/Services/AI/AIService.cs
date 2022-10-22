using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIService
{
    static Dictionary<string, AIBehaviour> _keyToBehaviour = new Dictionary<string, AIBehaviour>();

    static AIService()
    {
        _keyToBehaviour[AIBehaviours.PATROL] = new AIPatrolBehaviour();
    }

    public void UpdateBehaviour(AIModel model)
    {
        if(!_keyToBehaviour.TryGetValue(model.Behaviour.Key, out AIBehaviour behaviour))
        {
            return;
        }

        behaviour.Update(model);
    }
}
