using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        _aiService.UpdateBehaviour(ai);
    }
}
