using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNPCShipCommand : ICommand
{
    bool _isFriend;
    Vector3 _position;

    public SpawnNPCShipCommand(bool isFriend, Vector3 position)
    {
        _isFriend = isFriend;
        _position = position;
    }

    public void Execute(GameModel model)
    {
        var shipId = Guid.NewGuid();
        Game.Do(new SpawnShipCommand(shipId, _isFriend, _position));

        var ai = new AIModel();
        ai.AgentId = shipId;
        ai.Behaviour.Key = AIBehaviours.IDLE;
        model.AI.AddItem(ai);
        Game.AddUpdater(new AIBehaviourUpdater(ai.Id));

        //Game.Do(new AIStartPatrolCommand(ai.Id, new Vector3(10, 0, 0), new Vector3(-10, 0, 0)));
    }
}
