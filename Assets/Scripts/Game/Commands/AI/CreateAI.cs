using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

public class CreateAI : ICommand
{
    Guid _id;

    public CreateAI(Guid id)
    {
        _id = id;
    }

    public void Execute(GameModel model)
    {
        var ai = new AIModel();
        ai.Id = _id;
        model.AI.AddItem(ai);
        Game.AddUpdater(new AIBehaviourUpdater(ai.Id));
        new CreateVision(_id, 5).Execute(model);
    }
}
