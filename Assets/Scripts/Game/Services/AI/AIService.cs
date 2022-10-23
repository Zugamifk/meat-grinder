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

    public void UpdateBehaviour(GameModel model, AIModel ai)
    {
        if(!_keyToBehaviour.TryGetValue(ai.Behaviour.Key, out AIBehaviour behaviour))
        {
            return;
        }

        behaviour.Update(model, ai);
    }
}
