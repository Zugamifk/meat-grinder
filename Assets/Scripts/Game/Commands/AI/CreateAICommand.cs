using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAICommand : ICommand
{
    Guid _id;

    public CreateAICommand(Guid id)
    {
        _id = id;
    }

    public void Execute(GameModel model)
    {
        var ai = new AIModel();
        ai.Id = _id;
        ai.Behaviour.Key = AIBehaviours.IDLE;
        model.AI.AddItem(ai);
        Game.AddUpdater(new AIBehaviourUpdater(ai.Id));
        new CreateVisionCommand(_id, 5).Execute(model);
    }
}
