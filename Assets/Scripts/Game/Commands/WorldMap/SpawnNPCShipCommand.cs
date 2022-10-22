using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNPCShipCommand : ICommand
{
    public void Execute(GameModel model)
    {
        var id = Guid.NewGuid();
        Game.Do(new SpawnShipCommand(id));

        var ai = new AIModel();
        ai.AgentId = id;
        ai.Behaviour.TargetLocation = new Vector3(10, 0, 0);
        ai.Behaviour.Key = AIBehaviours.PATROL;
        model.AI.AddItem(ai);
        Game.AddUpdater(new AIBehaviourUpdater(ai.Id));
    }
}
